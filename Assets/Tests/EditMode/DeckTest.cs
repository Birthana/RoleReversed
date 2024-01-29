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

        // Act
        var card = deck.Draw();

        // Assert
        Assert.AreEqual(null, card);
    }
}
