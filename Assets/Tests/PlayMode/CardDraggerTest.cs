using Moq;
using System.Collections;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CardDraggerTest : MonoBehaviour
{
    private Hand hand;
    private GainActionManager gainActions;
    private RerollManager reroll;
    private SelectionScreen selectionScreen;
    private HoverAnimation hoverAnimation;

    [SetUp]
    public void Setup()
    {
        var gameObject = new GameObject();
        hand = gameObject.AddComponent<Hand>();
        selectionScreen = gameObject.AddComponent<SelectionScreen>();
        hoverAnimation = gameObject.AddComponent<HoverAnimation>();
    }

    [TearDown]
    public void TearDown()
    {
        FindObjectsOfType<Hand>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
        FindObjectsOfType<GainActionManager>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
        FindObjectsOfType<RerollManager>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
        FindObjectsOfType<SelectionScreen>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
        FindObjectsOfType<HoverAnimation>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
    }

    [UnityTest]
    public IEnumerator XXX()
    {
        // Arrange
        var mouse = new Mock<IMouseWrapper>();
        var card = new GameObject().AddComponent<Card>();
        mouse.Setup(x => x.PlayerPressesLeftClick()).Returns(true);
        mouse.Setup(x => x.IsOnHand()).Returns(true);
        mouse.Setup(x => x.GetHitComponent<Card>()).Returns(card);
        mouse.Setup(x => x.IsOnHand()).Returns(true);
        mouse.Setup(x => x.PlayerReleasesLeftClick()).Returns(true);
        mouse.Setup(x => x.IsOnSelection()).Returns(true);
        var cardDragger = new GameObject().AddComponent<CardDragger>();
        cardDragger.SetMouseWrapper(mouse.Object);
        yield return new WaitForEndOfFrame();

        // Act

        // Assert
        Assert.DoesNotThrow(() => cardDragger.UpdateLoop());
    }

}
