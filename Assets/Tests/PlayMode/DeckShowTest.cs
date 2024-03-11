using Moq;
using System.Collections;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class DeckShowTest : MonoBehaviour
{
    private DisplayCardInfos display;
    private Deck deck;

    [SetUp]
    public void Setup()
    {
        display = new GameObject().AddComponent<DisplayCardInfos>();
        deck = new GameObject().AddComponent<Deck>();
        deck.gameObject.AddComponent<DeckCount>();
        deck.gameObject.AddComponent<SpriteRenderer>();
    }

    [TearDown]
    public void TearDown()
    {
        FindObjectsOfType<Deck>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
        FindObjectsOfType<DisplayCardInfos>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
    }

    [UnityTest]
    public IEnumerator UsingDeck_Clicks_ExpectDeckCardIs1()
    {
        // Arrange
        deck.Add(TestHelper.GetAnyMonsterCardInfo());
        var mock = new Mock<IMouseWrapper>(MockBehavior.Strict);
        mock.Setup(x => x.PlayerPressesLeftClick()).Returns(true);
        mock.Setup(x => x.IsOnDeck()).Returns(true);
        deck.SetMouse(mock.Object);

        // Act
        deck.Update();

        mock = new Mock<IMouseWrapper>(MockBehavior.Strict);
        mock.Setup(x => x.PlayerPressesLeftClick()).Returns(false);
        deck.SetMouse(mock.Object);
        yield return null;

        // Assert
        mock.VerifyAll();
        var cardUIs = FindObjectsOfType<CardUI>();
        Assert.AreEqual(1, cardUIs.Length);
    }

    [UnityTest]
    public IEnumerator UsingDeckClickedOnce_Clicks_ExpectDeckCardIs0()
    {
        // Arrange
        var mock = new Mock<IMouseWrapper>(MockBehavior.Strict);
        mock.Setup(x => x.PlayerPressesLeftClick()).Returns(true);
        mock.Setup(x => x.IsOnDeck()).Returns(true);
        deck.SetMouse(mock.Object);
        deck.Update();

        // Act
        deck.Update();

        mock = new Mock<IMouseWrapper>(MockBehavior.Strict);
        mock.Setup(x => x.PlayerPressesLeftClick()).Returns(false);
        deck.SetMouse(mock.Object);
        yield return null;

        // Assert
        mock.VerifyAll();
        var cardUIs = FindObjectsOfType<CardUI>();
        Assert.AreEqual(0, cardUIs.Length);
    }
}
