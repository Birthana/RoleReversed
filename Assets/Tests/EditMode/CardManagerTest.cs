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

        // Act
        var monsterCard = cardManager.CreateCommonCard();

        // Assert
        Assert.AreEqual(true, cardManager.CardIsCommon(monsterCard));
    }

    [Test]
    public void GivenCardManager_GetRareCard_ExpectRareCard()
    {
        // Arrange
        var cardManager = TestHelper.GetCardManager();

        // Act
        var roomCard = cardManager.CreateRareCard();

        // Assert
        Assert.AreEqual(true, cardManager.CardIsRare(roomCard));
    }
}
