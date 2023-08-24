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
        selectionScreen = gameObject.AddComponent<SelectionScreen>();
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
        var cardDragger = new GameObject().AddComponent<CardDragger>();
        cardDragger.SetMouseWrapper(mock.Object);

        // Act
        cardDragger.UpdateLoop();
        yield return new WaitForEndOfFrame();

        // Assert
        mock.VerifyAll();
    }
}
