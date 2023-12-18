using Moq;
using NUnit.Framework;
using UnityEngine;

public class CardTest : MonoBehaviour
{
    private Player player;
    private MonsterCard card;
    private Hand hand;
    private CardDragger cardDragger;
    private GraySlime graySlimeInfo;
    private PinkySlime pinkySlimeInfo;
    private RedSlime redSlimeInfo;
    private VioletSlime violetSlimeInfo;
    private YellowSlime yellowSlimeInfo;
    private readonly int ANY_MAX_DAMAGE = 3;
    private readonly int ANY_MAX_HEALTH = 5;

    [SetUp]
    public void Setup()
    {
        player = TestHelper.GetPlayer(ANY_MAX_DAMAGE, ANY_MAX_HEALTH);
        card = new GameObject().AddComponent<MonsterCard>();
        hand = TestHelper.GetHand();
        cardDragger = TestHelper.GetCardDragger();
        CreateMonsterCardInfo();
    }

    public void CreateMonsterCardInfo()
    {
        graySlimeInfo = ScriptableObject.CreateInstance<GraySlime>();
        graySlimeInfo.damage = 3;
        graySlimeInfo.health = 3;

        pinkySlimeInfo = ScriptableObject.CreateInstance<PinkySlime>();
        pinkySlimeInfo.damage = 2;
        pinkySlimeInfo.health = 2;

        redSlimeInfo = ScriptableObject.CreateInstance<RedSlime>();
        redSlimeInfo.damage = 1;
        redSlimeInfo.health = 3;

        violetSlimeInfo = ScriptableObject.CreateInstance<VioletSlime>();
        violetSlimeInfo.damage = 1;
        violetSlimeInfo.health = 1;

        yellowSlimeInfo = ScriptableObject.CreateInstance<YellowSlime>();
        yellowSlimeInfo.damage = 5;
        yellowSlimeInfo.health = 5;
        yellowSlimeInfo.slimePrefab = new GameObject().AddComponent<Monster>();
    }

    [Test]
    public void UsingGraySlime_Exit_ExpectHandIs1AndStatIs3_3()
    {
        // Arrange
        var cardManager = new GameObject().AddComponent<CardManager>();
        cardManager.monsterCardPrefab = card.GetComponent<MonsterCard>();
        card.SetCardInfo(graySlimeInfo);
        cardManager.AddCommonCard(graySlimeInfo);
        cardManager.AddRareCard(graySlimeInfo);
        var newMonster = Instantiate(card.GetMonsterPrefab());
        newMonster.cardInfo = graySlimeInfo;
        newMonster.SetupStats(graySlimeInfo.GetDamage(), graySlimeInfo.GetHealth());

        // Act
        newMonster.Exit();

        // Assert
        Assert.AreEqual(1, hand.hand.Count);
        Assert.AreEqual(3, newMonster.GetComponent<Damage>().maxCount);
        Assert.AreEqual(3, newMonster.GetComponent<Health>().maxCount);
    }

    [Test]
    public void UsingPinkySlime_Exit_ExpectStatIs4_4()
    {
        // Arrange
        var cardManager = new GameObject().AddComponent<CardManager>();
        cardManager.monsterCardPrefab = card.GetComponent<MonsterCard>();
        card.SetCardInfo(pinkySlimeInfo);
        var newMonster = Instantiate(card.GetMonsterPrefab());
        newMonster.cardInfo = pinkySlimeInfo;
        var room = new GameObject().AddComponent<Room>();

        // Act
        newMonster.Entrance();
        newMonster.SetupStats(pinkySlimeInfo.GetDamage(), pinkySlimeInfo.GetHealth());

        // 
        Assert.AreEqual(4, newMonster.GetComponent<Damage>().maxCount);
        Assert.AreEqual(4, newMonster.GetComponent<Health>().maxCount);
    }

    [Test]
    public void UsingRedSlime_Exit_ExpectPlayerIsDealt2DamageAndStatIs1_3()
    {
        // Arrange
        var cardManager = new GameObject().AddComponent<CardManager>();
        cardManager.monsterCardPrefab = card.GetComponent<MonsterCard>();
        card.SetCardInfo(redSlimeInfo);
        var newMonster = Instantiate(card.GetMonsterPrefab());
        newMonster.cardInfo = redSlimeInfo;

        // Act
        newMonster.SetupStats(redSlimeInfo.GetDamage(), redSlimeInfo.GetHealth());
        newMonster.Exit();

        // 
        Assert.AreEqual(ANY_MAX_HEALTH - 2, player.GetComponent<Health>().GetCurrentHealth());
        Assert.AreEqual(1, newMonster.GetComponent<Damage>().maxCount);
        Assert.AreEqual(3, newMonster.GetComponent<Health>().maxCount);
    }

    [Test]
    public void UsingPurpleSlime_Exit_ExpectPlayerDamageIsReducedByOneAndStatIs1_1()
    {
        // Arrange
        var cardManager = new GameObject().AddComponent<CardManager>();
        cardManager.monsterCardPrefab = card.GetComponent<MonsterCard>();
        card.SetCardInfo(violetSlimeInfo);
        var newMonster = Instantiate(card.GetMonsterPrefab());
        newMonster.cardInfo = violetSlimeInfo;

        // Act
        newMonster.Entrance();
        newMonster.SetupStats(violetSlimeInfo.GetDamage(), violetSlimeInfo.GetHealth());

        // 
        Assert.AreEqual(ANY_MAX_DAMAGE - 1, player.GetComponent<Damage>().GetDamage());
        Assert.AreEqual(1, newMonster.GetComponent<Damage>().maxCount);
        Assert.AreEqual(1, newMonster.GetComponent<Health>().maxCount);
    }

    [Test]
    public void UsingYellowSlime_Exit_ExpectTwo2_2SlimesInRoomAndStatIs5_5()
    {
        // Arrange
        var room = TestHelper.GetRoom();
        player.transform.SetParent(room.transform);

        var cardManager = new GameObject().AddComponent<CardManager>();
        cardManager.monsterCardPrefab = card.GetComponent<MonsterCard>();
        card.SetCardInfo(yellowSlimeInfo);
        var newMonster = Instantiate(card.GetMonsterPrefab());
        newMonster.cardInfo = yellowSlimeInfo;
        newMonster.transform.SetParent(room.transform);

        // Act
        newMonster.SetupStats(yellowSlimeInfo.GetDamage(), yellowSlimeInfo.GetHealth());
        newMonster.Exit();

        //
        Assert.AreEqual(4, room.transform.childCount);
        Assert.AreEqual(5, newMonster.GetComponent<Damage>().maxCount);
        Assert.AreEqual(5, newMonster.GetComponent<Health>().maxCount);
    }
}
