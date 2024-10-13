using Moq;
using NUnit.Framework;
using UnityEngine;

public class CardManagerTest
{
    [Test]
    public void GivenCardManager_GetCommonCard_ExpectCommonCard()
    {
        // Arrange
        var cardManager = TestHelper.GetCardManager();
        var deck = TestHelper.GetDeck();
        deck.Draw();

        // Act
        deck.Add(cardManager.GetEasyCardInfo());
        var monsterCard = deck.Draw();

        // Assert
        Assert.AreEqual(true, cardManager.CardIsEasy(monsterCard));
    }
}
