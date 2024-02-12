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
        var deckCardPrefab = new GameObject().AddComponent<DisplayCard>();
        deckCardPrefab.gameObject.AddComponent<SpriteRenderer>();
        deckCardPrefab.gameObject.AddComponent<BoxCollider2D>();
        deckCardPrefab.gameObject.AddComponent<MonsterCardUI>();
        deckCardPrefab.gameObject.AddComponent<RoomCardUI>();
        display.displayCardPrefab = deckCardPrefab;
        deck = new GameObject().AddComponent<Deck>();
        deck.gameObject.AddComponent<DeckCount>();
        deck.Add(TestHelper.GetAnyMonsterCardInfo());
    }

    [TearDown]
    public void TearDown()
    {
        FindObjectsOfType<DraftCard>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
        FindObjectsOfType<Deck>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
        FindObjectsOfType<DisplayCardInfos>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
    }

    [UnityTest]
    public IEnumerator UsingDeck_Clicks_ExpectDeckCardIs1()
    {
        // Arrange
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
        var deckCard = FindObjectsOfType<DraftCard>();
        Assert.AreEqual(1 + 1, deckCard.Length);
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
        var deckCard = FindObjectsOfType<DraftCard>();
        Assert.AreEqual(0 + 1, deckCard.Length);
    }
}
