using Moq;
using NUnit.Framework;
using System.Linq;
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
    private BrownSlime brownSlimeInfo;
    private OrangeSlime orangeSlimeInfo;
    private TemporaryMonster temporarySlimeInfo;
    private Monster monsterPrefab;
    private readonly int ANY_MAX_DAMAGE = 3;
    private readonly int ANY_MAX_HEALTH = 5;
    private static readonly string FIELD_MONSTER_PREFAB = "Prefabs/FieldMonster";

    [SetUp]
    public void Setup()
    {
        player = TestHelper.GetPlayer(ANY_MAX_DAMAGE, ANY_MAX_HEALTH);
        card = new GameObject().AddComponent<MonsterCard>();
        hand = TestHelper.GetHand();
        cardDragger = TestHelper.GetCardDragger();
        monsterPrefab = Resources.Load<Monster>(FIELD_MONSTER_PREFAB);
        CreateMonsterCardInfo();
    }

    [TearDown]
    public void TearDown()
    {
        FindObjectsOfType<Room>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
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
        redSlimeInfo.health = 2;

        violetSlimeInfo = ScriptableObject.CreateInstance<VioletSlime>();
        violetSlimeInfo.damage = 1;
        violetSlimeInfo.health = 1;

        temporarySlimeInfo = ScriptableObject.CreateInstance<TemporaryMonster>();
        temporarySlimeInfo.damage = 2;
        temporarySlimeInfo.health = 2;

        yellowSlimeInfo = ScriptableObject.CreateInstance<YellowSlime>();
        yellowSlimeInfo.damage = 5;
        yellowSlimeInfo.health = 5;
        yellowSlimeInfo.tempMonsterCardInfo = temporarySlimeInfo;

        brownSlimeInfo = ScriptableObject.CreateInstance<BrownSlime>();
        brownSlimeInfo.damage = 2;
        brownSlimeInfo.health = 5;

        orangeSlimeInfo = ScriptableObject.CreateInstance<OrangeSlime>();
        orangeSlimeInfo.damage = 2;
        orangeSlimeInfo.health = 4;
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
        var newMonster = Instantiate(monsterPrefab);
        newMonster.cardInfo = graySlimeInfo;
        newMonster.Setup(graySlimeInfo.GetDamage(), graySlimeInfo.GetHealth());

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
        var newMonster = Instantiate(monsterPrefab);
        newMonster.cardInfo = pinkySlimeInfo;
        var room = new GameObject().AddComponent<Room>();

        // Act
        newMonster.Entrance();
        newMonster.Setup(pinkySlimeInfo.GetDamage(), pinkySlimeInfo.GetHealth());

        // 
        Assert.AreEqual(4, newMonster.GetComponent<Damage>().maxCount);
        Assert.AreEqual(4, newMonster.GetComponent<Health>().maxCount);
    }

    [Test]
    public void UsingRedSlime_Exit_ExpectPlayerIsDealt2DamageAndStatIs1_2()
    {
        // Arrange
        var cardManager = new GameObject().AddComponent<CardManager>();
        cardManager.monsterCardPrefab = card.GetComponent<MonsterCard>();
        card.SetCardInfo(redSlimeInfo);
        var newMonster = Instantiate(monsterPrefab);
        newMonster.cardInfo = redSlimeInfo;

        // Act
        newMonster.Setup(redSlimeInfo.GetDamage(), redSlimeInfo.GetHealth());
        newMonster.Exit();

        // 
        Assert.AreEqual(ANY_MAX_HEALTH - 2, player.GetComponent<Health>().GetCurrentHealth());
        Assert.AreEqual(1, newMonster.GetComponent<Damage>().maxCount);
        Assert.AreEqual(2, newMonster.GetComponent<Health>().maxCount);
    }

    [Test]
    public void UsingPurpleSlime_Exit_ExpectPlayerDamageIsReducedByOneAndStatIs1_1()
    {
        // Arrange
        var cardManager = new GameObject().AddComponent<CardManager>();
        cardManager.monsterCardPrefab = card.GetComponent<MonsterCard>();
        card.SetCardInfo(violetSlimeInfo);
        var newMonster = Instantiate(monsterPrefab);
        newMonster.cardInfo = violetSlimeInfo;

        // Act
        newMonster.Entrance();
        newMonster.Setup(violetSlimeInfo.GetDamage(), violetSlimeInfo.GetHealth());

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
        var newMonster = Instantiate(monsterPrefab);
        newMonster.cardInfo = yellowSlimeInfo;
        newMonster.transform.SetParent(room.transform);

        // Act
        newMonster.Setup(yellowSlimeInfo.GetDamage(), yellowSlimeInfo.GetHealth());
        newMonster.Exit();

        //
        Assert.AreEqual(4, room.transform.childCount);
        Assert.AreEqual(5, newMonster.GetComponent<Damage>().maxCount);
        Assert.AreEqual(5, newMonster.GetComponent<Health>().maxCount);
    }

    [Test]
    public void UsingBrownSlime_SetStats_ExpectAndStatIs2_5()
    {
        // Arrange
        var cardManager = new GameObject().AddComponent<CardManager>();
        cardManager.monsterCardPrefab = card.GetComponent<MonsterCard>();
        card.SetCardInfo(brownSlimeInfo);
        var newMonster = Instantiate(monsterPrefab);
        newMonster.cardInfo = brownSlimeInfo;

        // Act
        newMonster.Setup(brownSlimeInfo.GetDamage(), brownSlimeInfo.GetHealth());

        // 
        Assert.AreEqual(2, newMonster.GetComponent<Damage>().maxCount);
        Assert.AreEqual(5, newMonster.GetComponent<Health>().maxCount);
    }

    [Test]
    public void UsingBrownSlime_Engage_ExpectAndStatIs3_6()
    {
        // Arrange
        var cardManager = new GameObject().AddComponent<CardManager>();
        cardManager.monsterCardPrefab = card.GetComponent<MonsterCard>();
        card.SetCardInfo(brownSlimeInfo);
        var newMonster = Instantiate(monsterPrefab);
        var room = TestHelper.GetRoom();
        newMonster.transform.SetParent(room.transform);
        newMonster.cardInfo = brownSlimeInfo;

        // Act
        newMonster.Setup(brownSlimeInfo.GetDamage(), brownSlimeInfo.GetHealth());
        newMonster.Engage();

        // 
        Assert.AreEqual(3, newMonster.GetComponent<Damage>().maxCount);
        Assert.AreEqual(6, newMonster.GetComponent<Health>().maxCount);
    }

    [Test]
    public void UsingOrangeSlime_SetStats_ExpectAndStatIs2_4()
    {
        // Arrange
        var cardManager = new GameObject().AddComponent<CardManager>();
        cardManager.monsterCardPrefab = card.GetComponent<MonsterCard>();
        card.SetCardInfo(orangeSlimeInfo);
        var newMonster = Instantiate(monsterPrefab);
        newMonster.cardInfo = orangeSlimeInfo;

        // Act
        newMonster.Setup(orangeSlimeInfo.GetDamage(), orangeSlimeInfo.GetHealth());

        // 
        Assert.AreEqual(2, newMonster.GetComponent<Damage>().maxCount);
        Assert.AreEqual(4, newMonster.GetComponent<Health>().maxCount);
    }

    [Test]
    public void UsingTemporarySlime_SetStats_ExpectAndStatIs2_2()
    {
        // Arrange
        var cardManager = new GameObject().AddComponent<CardManager>();
        cardManager.monsterCardPrefab = card.GetComponent<MonsterCard>();
        card.SetCardInfo(temporarySlimeInfo);
        var newMonster = Instantiate(monsterPrefab);
        newMonster.cardInfo = temporarySlimeInfo;

        // Act
        newMonster.Setup(temporarySlimeInfo.GetDamage(), temporarySlimeInfo.GetHealth());

        // 
        Assert.AreEqual(2, newMonster.GetComponent<Damage>().maxCount);
        Assert.AreEqual(2, newMonster.GetComponent<Health>().maxCount);
        Assert.AreEqual(true, newMonster.isTemporary);
    }
}
