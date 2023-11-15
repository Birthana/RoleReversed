using Moq;
using NUnit.Framework;
using UnityEngine;

public class CardManagerTest
{
    [Test]
    public void GivenCardManager_GetCommonCard_ExpectCommonCard()
    {
        // Arrange
        var card = new GameObject().AddComponent<Card>();
        var cardManager = new GameObject().AddComponent<CardManager>();
        cardManager.AddCommonCard(card);

        // Act
        var monsterCard = cardManager.CreateCommonCard();

        // Assert
        Assert.AreEqual(true, cardManager.CardIsCommon(monsterCard));
    }

    [Test]
    public void GivenCardManager_GetRareCard_ExpectRareCard()
    {
        // Arrange
        var card = new GameObject().AddComponent<Card>();
        var cardManager = new GameObject().AddComponent<CardManager>();
        cardManager.AddRareCard(card);

        // Act
        var monsterCard = cardManager.CreateRareCard();

        // Assert
        Assert.AreEqual(true, cardManager.CardIsRare(monsterCard));
    }
}
