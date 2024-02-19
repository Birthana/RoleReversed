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

    public bool CheckCardListsAreSame(List<Card> list1, List<Card> list2)
    {
        for (var index = 0; index < list1.Count; index++)
        {
            if (list1[index].gameObject != list2[index].gameObject)
            {
                return false;
            }
        }

        return true;
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
        Assert.AreEqual(true, CheckCardListsAreSame(expectedCard, hand.hand));
    }
}
