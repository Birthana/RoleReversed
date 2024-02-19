using Moq;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class DeckTest : MonoBehaviour
{
    private CardManager cardManager;
    private CardDragger cardDragger;
    private Hand hand;

    [SetUp]
    public void Setup()
    {
        cardManager = TestHelper.GetCardManager();
        cardDragger = TestHelper.GetCardDragger();
        hand = TestHelper.GetHand();
    }

    [TearDown]
    public void TearDown()
    {
        FindObjectsOfType<CardManager>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
        FindObjectsOfType<Deck>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
        FindObjectsOfType<Card>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
        FindObjectsOfType<MonsterCard>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
        FindObjectsOfType<RoomCard>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
    }

    [Test]
    public void UsingDeck_CreateCard_DeckSizeIsOne()
    {
        // Arrange
        var deck = TestHelper.GetDeck();
        var cardInfo = TestHelper.GetAnyMonsterCardInfo();

        // Act
        deck.Add(cardInfo);

        // Assert
        Assert.AreEqual(1, deck.GetSize());
    }

    [Test]
    public void UsingDeckWith1Card_Draw_HandSizeIsOneAndDeckSizeIsZero()
    {
        // Arrange
        var deck = TestHelper.GetDeck();
        var cardInfo = TestHelper.GetAnyMonsterCardInfo();
        deck.Add(cardInfo);

        // Act
        var card = deck.Draw();

        // Assert
        Assert.AreEqual(TestHelper.ANY_CARD_NAME_1, card.GetName());
        Assert.AreEqual(0, deck.GetSize());
    }

    [Test]
    public void UsingDeck_Draw_CardIsZero()
    {
        // Arrange
        var deck = TestHelper.GetDeck();
        var drop = new GameObject().AddComponent<Drop>();
        var displayCard = new GameObject().AddComponent<DisplayCard>();
        displayCard.gameObject.AddComponent<MonsterCardUI>();
        displayCard.gameObject.AddComponent<RoomCardUI>();
        drop.SetDisplayCard(displayCard);
        drop.gameObject.AddComponent<SpriteRenderer>();
        drop.frontDeckBox = new GameObject().AddComponent<SpriteRenderer>();

        // Act
        var card = deck.Draw();

        // Assert
        Assert.AreEqual(null, card);
    }

    [Test]
    public void UsingDeck_DrawCardToHand_CardIsZero()
    {
        // Arrange
        hand.Add(new GameObject().AddComponent<Card>());
        hand.Add(new GameObject().AddComponent<Card>());
        hand.Add(new GameObject().AddComponent<Card>());
        hand.Add(new GameObject().AddComponent<Card>());
        hand.Add(new GameObject().AddComponent<Card>());
        hand.Add(new GameObject().AddComponent<Card>());
        hand.Add(new GameObject().AddComponent<Card>());
        hand.Add(new GameObject().AddComponent<Card>());
        var deck = TestHelper.GetDeck();
        var cardInfo = TestHelper.GetAnyMonsterCardInfo();
        deck.Add(cardInfo);

        // Act
        deck.DrawCardToHand();

        // Assert
        Assert.AreEqual(1, deck.GetSize());
    }
}
