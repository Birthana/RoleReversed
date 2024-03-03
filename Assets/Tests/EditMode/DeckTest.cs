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

    public void FillHand()
    {
        for (int i = 0; i < 8; i++)
        {
            hand.Add(new GameObject().AddComponent<Card>());
        }
    }

    [Test]
    public void UsingDeck_CreateCard_DeckSizeIsOne()
    {
        // Arrange
        var deck = TestHelper.GetDeck();

        // Act

        // Assert
        Assert.AreEqual(1, deck.GetSize());
    }

    [Test]
    public void UsingDeckWith1Card_Draw_HandSizeIsOneAndDeckSizeIsZero()
    {
        // Arrange
        var deck = TestHelper.GetDeck();
        deck.Draw();
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
        deck.Draw();
        var drop = TestHelper.GetDrop();

        // Act
        var card = deck.Draw();

        // Assert
        Assert.AreEqual(null, card);
    }

    [Test]
    public void UsingDeck_DrawCardToHand_CardIsZero()
    {
        // Arrange
        FillHand();
        var deck = TestHelper.GetDeck();
        deck.Draw();
        var cardInfo = TestHelper.GetAnyMonsterCardInfo();
        deck.Add(cardInfo);

        // Act
        deck.DrawCardToHand();

        // Assert
        Assert.AreEqual(1, deck.GetSize());
    }
}
