using Moq;
using NUnit.Framework;
using UnityEngine;

public class CardManagerTest
{
    [Test]
    public void GivenCardManager_GetCommonCard_ExpectCommonCard()
    {
        // Arrange
        var card = new GameObject();
        card.AddComponent<MonsterCard>();
        card.AddComponent<SpriteRenderer>();
        var cardManager = new GameObject().AddComponent<CardManager>();
        cardManager.monsterCardPrefab = card.GetComponent<MonsterCard>();
        var monsterPrefab = new GameObject();
        monsterPrefab.AddComponent<Monster>();
        var cardInfo = ScriptableObject.CreateInstance<CardInfo>();
        cardInfo.prefab = monsterPrefab;
        cardManager.AddCommonCard(cardInfo);

        // Act
        var monsterCard = cardManager.CreateCommonCard();

        // Assert
        Assert.AreEqual(true, cardManager.CardIsCommon(monsterCard));
    }

    [Test]
    public void GivenCardManager_GetRareCard_ExpectRareCard()
    {
        // Arrange
        var card = new GameObject();
        card.AddComponent<RoomCard>();
        card.AddComponent<SpriteRenderer>();
        var cardManager = new GameObject().AddComponent<CardManager>();
        cardManager.roomCardPrefab = card.GetComponent<RoomCard>();
        var cardInfo = ScriptableObject.CreateInstance<CardInfo>();
        var roomPrefab = new GameObject();
        roomPrefab.AddComponent<Room>();
        cardInfo.prefab = roomPrefab;
        cardManager.AddRareCard(cardInfo);

        // Act
        var roomCard = cardManager.CreateRareCard();

        // Assert
        Assert.AreEqual(true, cardManager.CardIsRare(roomCard));
    }
}
