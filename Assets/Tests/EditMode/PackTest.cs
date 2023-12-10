using Moq;
using NUnit.Framework;
using UnityEngine;

public class PackTest
{
    private Mock<IMouseWrapper> mock;
    private CardManager cardManager;
    private Hand hand;
    private CardDragger cardDragger;

    [SetUp]
    public void Setup()
    {
        cardManager = new GameObject().AddComponent<CardManager>();
        var monsterCard = new GameObject().AddComponent<MonsterCard>();
        monsterCard.gameObject.AddComponent<SpriteRenderer>();
        cardManager.monsterCardPrefab = monsterCard;
        var roomCard = new GameObject().AddComponent<RoomCard>();
        roomCard.gameObject.AddComponent<SpriteRenderer>();
        cardManager.roomCardPrefab = roomCard;
        var monsterCardInfo = ScriptableObject.CreateInstance<MonsterCardInfo>();
        cardManager.AddCommonCard(monsterCardInfo);
        var roomCardInfo = ScriptableObject.CreateInstance<RoomCardInfo>();
        cardManager.AddRareCard(roomCardInfo);
        hand = new GameObject().AddComponent<Hand>();
        cardDragger = new GameObject().AddComponent<CardDragger>();
        cardDragger.gameObject.AddComponent<HoverAnimation>();
        cardDragger.Awake();
        mock = new Mock<IMouseWrapper>(MockBehavior.Strict);
    }

    [Test]
    public void UsingPack_CreateRarityPack_Expect5Cards()
    {
        // Arrange
        var pack = new GameObject().AddComponent<Pack>();

        // Act
        pack.CreateRarityPack();

        // Assert
        Assert.AreEqual(5, pack.GetSize());
    }

    [Test]
    public void UsingPack_CreateStarterPack_Expect2Cards()
    {
        // Arrange
        var pack = new GameObject().AddComponent<Pack>();

        // Act
        pack.CreateStarterPack();

        // Assert
        Assert.AreEqual(2, pack.GetSize());
    }

    [Test]
    public void UsingPack_ClickOnPack_Expect2CardsInHand()
    {
        // Arrange
        var pack = new GameObject().AddComponent<Pack>();
        mock.Setup(x => x.PlayerPressesLeftClick()).Returns(true);
        mock.Setup(x => x.IsOnPack()).Returns(true);
        pack.SetMouseWrapper(mock.Object);
        pack.CreateStarterPack();

        // Act
        pack.Update();

        // Assert
        Assert.AreEqual(2, hand.hand.Count);
    }
}
