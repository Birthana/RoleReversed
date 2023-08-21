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
        mock = new Mock<IMouseWrapper>();
    }

    public Mock<IMouseWrapper> Get()
    {
        return mock;
    }

    public TestSetup WithPlayerClicksOnHand()
    {
        mock.Setup(x => x.PlayerPressesLeftClick()).Returns(true);
        mock.Setup(x => x.IsOnHand()).Returns(true);
        return this;
    }

    public TestSetup WithPlayerDoesNotClickOnHand()
    {
        mock.Setup(x => x.PlayerPressesLeftClick()).Returns(false);
        return this;
    }

    public TestSetup WithPickedUpCard(Card card)
    {
        mock.Setup(x => x.GetHitComponent<Card>()).Returns(card);
        WithNoHover();
        return this;
    }

    private TestSetup WithNoHover()
    {
        mock.Setup(x => x.IsOnHand()).Returns(true);
        return this;
    }

    public TestSetup WithHover(Card card)
    {
        mock.Setup(x => x.IsOnHand()).Returns(true);
        mock.Setup(x => x.GetHitComponent<Card>()).Returns(card);
        return this;
    }

    public TestSetup WithAddCardToSelection()
    {
        mock.Setup(x => x.PlayerReleasesLeftClick()).Returns(true);
        mock.Setup(x => x.IsOnSelection()).Returns(true);
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
    }

    [UnityTest]
    public IEnumerator GivenClicksOnHandPickACardAndAddToSelection_Update_ExpectNoErrors()
    {
        // Arrange
        var card = new GameObject().AddComponent<Card>();
        var mouse = new TestSetup().WithPlayerClicksOnHand().WithPickedUpCard(card).WithAddCardToSelection().Get();
        var cardDragger = new GameObject().AddComponent<CardDragger>();
        cardDragger.SetMouseWrapper(mouse.Object);
        yield return new WaitForEndOfFrame();

        // Act

        // Assert
        Assert.DoesNotThrow(() => cardDragger.UpdateLoop());
    }

    [UnityTest]
    public IEnumerator GivenNoSelectedCards_Update_ExpectNoErrors()
    {
        // Arrange
        var card = new GameObject().AddComponent<Card>();
        var mouse = new TestSetup().WithPlayerDoesNotClickOnHand().WithHover(card).Get();
        var gameObject = new GameObject();
        gameObject.AddComponent<HoverAnimation>();
        var cardDragger = gameObject.AddComponent<CardDragger>();
        cardDragger.SetMouseWrapper(mouse.Object);
        yield return new WaitForEndOfFrame();

        // Act

        // Assert
        Assert.DoesNotThrow(() => cardDragger.UpdateLoop());
    }
}
