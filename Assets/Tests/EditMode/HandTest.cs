using Moq;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class HandTest : MonoBehaviour
{
    private Card firstCard;
    private Card secondCard;
    private Card thirdCard;

    [SetUp]
    public void Setup()
    {
        firstCard = new GameObject().AddComponent<MonsterCard>();
        secondCard = new GameObject().AddComponent<RoomCard>();
        thirdCard = new GameObject().AddComponent<MonsterCard>();
    }

    [TearDown]
    public void TearDown()
    {
        FindObjectsOfType<Card>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
        FindObjectsOfType<CardDragger>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
    }

    [Test]
    public void GivenHandWithThreeCards_RemoveThirdCard_ExpectFirstAndSecondCard()
    {
        // Arrange
        var hand = TestHelper.GetHand();
        var cardDragger = TestHelper.GetCardDragger();
        hand.Add(firstCard);
        hand.Add(secondCard);
        hand.Add(thirdCard);

        // Act
        hand.Remove(thirdCard);

        // Assert
        List<Card> expectedCard = new List<Card>();
        expectedCard.Add(firstCard);
        expectedCard.Add(secondCard);
        for(var index = 0; index < expectedCard.Count; index++)
        {
            Assert.AreEqual(expectedCard[index].gameObject, hand.hand[index].gameObject);
        }
    }
}
