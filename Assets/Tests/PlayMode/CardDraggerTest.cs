using Moq;
using System.Collections;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestSetup
{
    private Mock<IMouseWrapper> mock;

    public TestSetup()
    {
        mock = new Mock<IMouseWrapper>(MockBehavior.Strict);
    }

    public Mock<IMouseWrapper> Get()
    {
        return mock;
    }

    public TestSetup WithPlayerSelectsCard(Card card)
    {
        mock.Setup(x => x.PlayerPressesLeftClick()).Returns(true);
        mock.Setup(x => x.IsOnHand()).Returns(true);
        mock.Setup(x => x.GetHitComponent<Card>()).Returns(card);
        return this;
    }

    public TestSetup WithPlayerDoesNotSelectCard()
    {
        mock.Setup(x => x.PlayerPressesLeftClick()).Returns(false);
        return this;
    }

    public TestSetup WithPlayerHoversCard(Card card)
    {
        mock.Setup(x => x.IsOnHand()).Returns(true);
        mock.Setup(x => x.GetHitComponent<Card>()).Returns(card);
        return this;
    }

    public TestSetup WithPlayerAddsToSelection()
    {
        mock.Setup(x => x.PlayerReleasesLeftClick()).Returns(true);
        mock.Setup(x => x.IsOnSelection()).Returns(true);
        return this;
    }

    public TestSetup WithPlayerDoesNotAddToSelection()
    {
        mock.Setup(x => x.PlayerReleasesLeftClick()).Returns(false);
        return this;
    }
}

public class CardDraggerTest : MonoBehaviour
{
    private Hand hand;
    private SelectionScreen selectionScreen;

    [SetUp]
    public void Setup()
    {
        var gameObject = new GameObject();
        hand = gameObject.AddComponent<Hand>();
        hand.SPACING = 5.0f;
        selectionScreen = gameObject.AddComponent<SelectionScreen>();
        selectionScreen.transform.position = new Vector2(3, 5);
        selectionScreen.SetMaxSelection(3);
    }

    [TearDown]
    public void TearDown()
    {
        FindObjectsOfType<Hand>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
        FindObjectsOfType<SelectionScreen>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
        FindObjectsOfType<Card>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
        FindObjectsOfType<CardDragger>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
    }

    [UnityTest]
    public IEnumerator GivenNoSelectedCards_Update_ExpectNoErrors()
    {
        // Arrange
        var card = new GameObject().AddComponent<Card>();
        var mock = new TestSetup().WithPlayerDoesNotSelectCard().WithPlayerHoversCard(card).WithPlayerDoesNotAddToSelection().Get();
        var gameObject = new GameObject();
        gameObject.AddComponent<HoverAnimation>();
        var cardDragger = gameObject.AddComponent<CardDragger>();
        cardDragger.SetMouseWrapper(mock.Object);

        // Act
        cardDragger.UpdateLoop();
        yield return new WaitForEndOfFrame();

        // Assert
        mock.VerifyAll();
    }

    [UnityTest]
    public IEnumerator GivenClicksOnHandPickACardAndAddToSelection_Update_ExpectNoErrors()
    {
        // Arrange
        var card = new GameObject().AddComponent<Card>();
        var mock = new TestSetup().WithPlayerSelectsCard(card).WithPlayerAddsToSelection().Get();
        var gameObject = new GameObject();
        gameObject.AddComponent<HoverAnimation>();
        var cardDragger = gameObject.AddComponent<CardDragger>();
        cardDragger.SetMouseWrapper(mock.Object);

        // Act
        cardDragger.UpdateLoop();
        yield return new WaitForEndOfFrame();

        // Assert
        mock.VerifyAll();
    }

    [UnityTest]
    public IEnumerator GivenHoverDrag_AddToSelection_ExpectPositionIsCorrect()
    {
        // Arrange
        var card = new GameObject().AddComponent<Card>();
        var mock = new Mock<IMouseWrapper>(MockBehavior.Strict);
        mock.SetupSequence(x => x.PlayerPressesLeftClick()).Returns(false).Returns(true);
        mock.Setup(x => x.IsOnHand()).Returns(true);
        mock.Setup(x => x.GetHitComponent<Card>()).Returns(card);
        mock.SetupSequence(x => x.PlayerReleasesLeftClick()).Returns(false).Returns(true);
        mock.Setup(x => x.IsOnSelection()).Returns(true);
        var gameObject = new GameObject();
        gameObject.AddComponent<HoverAnimation>();
        var cardDragger = gameObject.AddComponent<CardDragger>();
        cardDragger.SetMouseWrapper(mock.Object);
        cardDragger.UpdateLoop();
        yield return new WaitForSeconds(1.0f);
        cardDragger.UpdateLoop();

        // Act
        cardDragger.UpdateLoop();

        // Assert
        mock.VerifyAll();
        var hoverMovement = new Vector3(0, 0.5f, 0);
        Assert.AreEqual(selectionScreen.transform.position + hoverMovement, card.transform.position);
    }

    [UnityTest]
    public IEnumerator GivenHoverDragAddToSelection_Hover_ExpectPositionIsCorrect()
    {
        // Arrange
        var card = new GameObject().AddComponent<Card>();
        var card2 = new GameObject().AddComponent<Card>();
        hand.Add(card);
        hand.Add(card2);
        var previousPosition = card2.transform.position;
        var mock = new Mock<IMouseWrapper>(MockBehavior.Strict);
        mock.SetupSequence(x => x.PlayerPressesLeftClick()).Returns(false).Returns(true).Returns(false).Returns(false);
        mock.Setup(x => x.IsOnHand()).Returns(true);
        mock.Setup(x => x.GetHitComponent<Card>()).Returns(card);
        mock.SetupSequence(x => x.PlayerReleasesLeftClick()).Returns(false).Returns(true);
        mock.Setup(x => x.IsOnSelection()).Returns(true);
        var gameObject = new GameObject();
        gameObject.AddComponent<HoverAnimation>();
        var cardDragger = gameObject.AddComponent<CardDragger>();
        cardDragger.SetMouseWrapper(mock.Object);
        cardDragger.UpdateLoop();
        yield return new WaitForSeconds(1.0f);
        cardDragger.UpdateLoop();


        // Act
        cardDragger.UpdateLoop();
        selectionScreen.ReturnAllSelections();
        mock.Setup(x => x.GetHitComponent<Card>()).Returns(card2);
        cardDragger.SetMouseWrapper(mock.Object);
        cardDragger.UpdateLoop();


        // Assert
        mock.VerifyAll();
        Assert.AreEqual(previousPosition, card.transform.position);
    }
}
