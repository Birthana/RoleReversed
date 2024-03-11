using Moq;
using NUnit.Framework;
using System.Linq;
using UnityEngine;

public class CardTest : MonoBehaviour
{
    private Player player;
    private Hand hand;
    private Deck deck;
    private Room room;
    private CardManager cardManager;
    private CardDragger cardDragger;
    private GraySlime graySlimeInfo;
    private PinkySlime pinkySlimeInfo;
    private RedSlime redSlimeInfo;
    private VioletSlime violetSlimeInfo;
    private YellowSlime yellowSlimeInfo;
    private BrownSlime brownSlimeInfo;
    private OrangeSlime orangeSlimeInfo;
    private EmeraldSlime emeraldSlimeInfo;
    private GoblinWorker goblinWorkerInfo;
    private GoblinMiner goblinMinerInfo;
    private GoblinBuilder goblinBuilderInfo;
    private GoblinGatherer goblinGathererInfo;
    private RedBrownSlime redBrownSlimeInfo;
    private OrangeGraySlime orangeGraySlimeInfo;
    private GreenOrangeSlime greenOrangeSlimeInfo;
    private GoblinTailor goblinTailorInfo;
    private TemporaryMonster temporarySlimeInfo;
    private readonly int ANY_MAX_DAMAGE = 3;
    private readonly int ANY_MAX_HEALTH = 5;

    [SetUp]
    public void Setup()
    {
        player = TestHelper.GetPlayer(ANY_MAX_DAMAGE, ANY_MAX_HEALTH);
        hand = TestHelper.GetHand();
        cardManager = TestHelper.GetCardManager();
        cardDragger = TestHelper.GetCardDragger();
        CreateMonsterCardInfo();
        deck = TestHelper.GetDeck();
        deck.Add(graySlimeInfo);
        room = TestHelper.GetRoom();
    }

    [TearDown]
    public void TearDown()
    {
        FindObjectsOfType<Room>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
        FindObjectsOfType<CardManager>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
        FindObjectsOfType<Deck>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
        FindObjectsOfType<Room>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
    }

    public void CreateMonsterCardInfo()
    {
        graySlimeInfo = ScriptableObject.CreateInstance<GraySlime>();
        graySlimeInfo.damage = 3;
        graySlimeInfo.health = 3;
        graySlimeInfo.effectDescription = "1";

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

        emeraldSlimeInfo = ScriptableObject.CreateInstance<EmeraldSlime>();
        emeraldSlimeInfo.damage = 1;
        emeraldSlimeInfo.health = 1;
        emeraldSlimeInfo.tempMonsterCardInfo = temporarySlimeInfo;

        goblinWorkerInfo = ScriptableObject.CreateInstance<GoblinWorker>();
        goblinWorkerInfo.damage = 1;
        goblinWorkerInfo.health = 2;

        goblinMinerInfo = ScriptableObject.CreateInstance<GoblinMiner>();
        goblinMinerInfo.damage = 3;
        goblinMinerInfo.health = 3;

        goblinBuilderInfo = ScriptableObject.CreateInstance<GoblinBuilder>();
        goblinBuilderInfo.damage = 5;
        goblinBuilderInfo.health = 3;

        goblinGathererInfo = ScriptableObject.CreateInstance<GoblinGatherer>();
        goblinGathererInfo.damage = 2;
        goblinGathererInfo.health = 1;

        redBrownSlimeInfo = ScriptableObject.CreateInstance<RedBrownSlime>();
        redBrownSlimeInfo.damage = 1;
        redBrownSlimeInfo.health = 2;

        orangeGraySlimeInfo = ScriptableObject.CreateInstance<OrangeGraySlime>();
        orangeGraySlimeInfo.damage = 4;
        orangeGraySlimeInfo.health = 10;

        greenOrangeSlimeInfo = ScriptableObject.CreateInstance<GreenOrangeSlime>();
        greenOrangeSlimeInfo.damage = 3;
        greenOrangeSlimeInfo.health = 5;

        goblinTailorInfo = ScriptableObject.CreateInstance<GoblinTailor>();
        goblinTailorInfo.damage = 1;
        goblinTailorInfo.health = 2;
    }
    public bool RoomMonstersAreInCorrectLayer(Room room, string layer)
    {
        foreach (var monster in room.monsters)
        {
            if (!monster.isTemporary)
            {
                continue;
            }

            if (layer != monster.GetComponent<SpriteRenderer>().sortingLayerName)
            {
                return false;
            }
        }

        return true;
    }

    [Test]
    public void UsingGraySlime_Exit_ExpectHandIs1AndStatIs3_3()
    {
        // Arrange
        var graySlime = room.SpawnMonster(graySlimeInfo);

        // Act
        graySlime.Exit();

        // Assert
        Assert.AreEqual(1, hand.GetSize());
        Assert.AreEqual(3, graySlime.GetDamage());
        Assert.AreEqual(3, graySlime.GetHealth());
    }

    [Test]
    public void UsingPinkySlime_Exit_ExpectStatIs4_4()
    {
        // Arrange

        // Act
        var pinkSlime = room.SpawnMonster(pinkySlimeInfo);

        // Assert 
        Assert.AreEqual(4, pinkSlime.GetDamage());
        Assert.AreEqual(4, pinkSlime.GetHealth());
    }

    [Test]
    public void UsingRedSlime_Exit_ExpectPlayerIsDealt2DamageAndStatIs1_2()
    {
        // Arrange
        var redSlime = room.SpawnMonster(redSlimeInfo);

        // Act
        redSlime.Exit();

        // Assert
        Assert.AreEqual(ANY_MAX_HEALTH - 2, player.GetHealthComponent().GetCurrentHealth());
        Assert.AreEqual(1, redSlime.GetDamage());
        Assert.AreEqual(2, redSlime.GetHealth());
    }

    [Test]
    public void UsingPurpleSlime_Exit_ExpectPlayerDamageIsReducedByOneAndStatIs1_1()
    {
        // Arrange

        // Act
        var violetSlime = room.SpawnMonster(violetSlimeInfo);

        // Assert
        Assert.AreEqual(ANY_MAX_DAMAGE - 1, player.GetDamage());
        Assert.AreEqual(1, violetSlime.GetDamage());
        Assert.AreEqual(1, violetSlime.GetHealth());
    }

    [Test]
    public void UsingYellowSlime_Exit_ExpectTwo2_2SlimesInRoomAndStatIs5_5()
    {
        // Arrange
        player.transform.SetParent(room.transform);
        var yellowSlime = room.SpawnMonster(yellowSlimeInfo);

        // Act
        yellowSlime.Exit();

        // Assert
        Assert.AreEqual(4, room.transform.childCount);
        Assert.AreEqual(5, yellowSlime.GetDamage());
        Assert.AreEqual(5, yellowSlime.GetHealth());
    }

    [Test]
    public void UsingBrownSlime_SetStats_ExpectAndStatIs2_5()
    {
        // Arrange
        var brownSlime = room.SpawnMonster(brownSlimeInfo);

        // Act

        // Assert
        Assert.AreEqual(2, brownSlime.GetDamage());
        Assert.AreEqual(5, brownSlime.GetHealth());
    }

    [Test]
    public void UsingBrownSlime_Engage_ExpectAndStatIs3_6()
    {
        // Arrange
        var brownSlime = room.SpawnMonster(brownSlimeInfo);

        // Act
        brownSlime.Engage();

        // Assert
        Assert.AreEqual(3, brownSlime.GetDamage());
        Assert.AreEqual(6, brownSlime.GetHealth());
    }

    [Test]
    public void UsingOrangeSlime_SetStats_ExpectAndStatIs2_4()
    {
        // Arrange

        // Act
        var orangeSlime = room.SpawnMonster(orangeSlimeInfo);

        // Assert 
        Assert.AreEqual(2, orangeSlime.GetDamage());
        Assert.AreEqual(4, orangeSlime.GetHealth());
    }

    [Test]
    public void UsingTemporarySlime_SetStats_ExpectAndStatIs2_2()
    {
        // Arrange

        // Act
        var temporarySlime = room.SpawnTemporaryMonster(temporarySlimeInfo);

        // Assert 
        Assert.AreEqual(2, temporarySlime.GetDamage());
        Assert.AreEqual(2, temporarySlime.GetHealth());
        Assert.AreEqual(true, temporarySlime.isTemporary);
    }

    [Test]
    public void UsingEmeraldSlime_Exit_Expect12_2SlimeInRoomAndStatIs1_1()
    {
        // Arrange
        player.transform.SetParent(room.transform);
        var emeraldSlime = room.SpawnMonster(emeraldSlimeInfo);

        // Act
        emeraldSlime.Exit();

        // Assert
        Assert.AreEqual(3, room.transform.childCount);
        Assert.AreEqual(1, emeraldSlime.GetDamage());
        Assert.AreEqual(1, emeraldSlime.GetHealth());
        Assert.AreEqual(true, RoomMonstersAreInCorrectLayer(room, "CurrentRoom"));
    }

    [Test]
    public void UsingGoblinWorker_Entrance_ExpectRoomCapacityPlus1AndStatIs1_2()
    {
        // Arrange

        // Act
        var goblinWorker = room.SpawnMonster(goblinWorkerInfo);

        // Assert
        Assert.AreEqual(2, room.GetCapacity());
        Assert.AreEqual(1, goblinWorker.GetDamage());
        Assert.AreEqual(2, goblinWorker.GetHealth());
    }

    [Test]
    public void UsingGoblinMiner_Engage_ExpectReduceRoomCapacityAndStatIs3_3()
    {
        // Arrange
        var goblinMiner = room.SpawnMonster(goblinMinerInfo);

        // Act
        goblinMiner.Engage();

        // Assert
        Assert.AreEqual(0, room.GetCapacity());
        Assert.AreEqual(1, hand.GetSize());
        Assert.AreEqual(5, goblinMiner.GetDamage());
        Assert.AreEqual(5, goblinMiner.GetHealth());
    }

    [Test]
    public void UsingGoblinBuilder_Entrance_ExpectRoomCapacityPlus2AndStatIs5_3()
    {
        // Arrange

        // Act
        var goblinBuilder = room.SpawnMonster(goblinBuilderInfo);

        // Assert
        Assert.AreEqual(3, room.GetCapacity());
        Assert.AreEqual(5, goblinBuilder.GetDamage());
        Assert.AreEqual(3, goblinBuilder.GetHealth());
    }

    [Test]
    public void UsingGoblinGatherer_Entrance_ExpectStatIs3_2()
    {
        // Arrange
        var actionManager = new GameObject().AddComponent<ActionManager>();

        // Act
        var goblinGatherer = room.SpawnMonster(goblinGathererInfo);

        // Assert
        Assert.AreEqual(1, actionManager.GetCount());
        Assert.AreEqual(3, goblinGatherer.GetDamage());
        Assert.AreEqual(2, goblinGatherer.GetHealth());
    }

    [Test]
    public void UsingRedBrownSlime_Exit_ExpectStatIs1_2()
    {
        // Arrange
        var redBrownSlime = room.SpawnMonster(redBrownSlimeInfo);

        // Act
        redBrownSlime.Exit();

        // Assert
        Assert.AreEqual(2, redBrownSlime.GetDamage());
        Assert.AreEqual(3, redBrownSlime.GetHealth());
    }

    [Test]
    public void UsingGoblinBuilderInRoomCapacity9_Entrance_ExpectRoomCapacityIs8Plus2AndStatIs5_3()
    {
        // Arrange
        room.SetCapacity(9);

        // Act
        var goblinBuilder = room.SpawnMonster(goblinBuilderInfo);

        // Assert
        Assert.AreEqual(9 - 1, room.GetCapacity());
        Assert.AreEqual(5, goblinBuilder.GetDamage());
        Assert.AreEqual(3, goblinBuilder.GetHealth());
    }

    [Test]
    public void UsingGoblinBuilderInRoomCapacity8_Entrance_ExpectRoomCapacityIs8Plus2AndStatIs5_3()
    {
        // Arrange
        room.SetCapacity(8);

        // Act
        var goblinBuilder = room.SpawnMonster(goblinBuilderInfo);

        // Assert
        Assert.AreEqual(9 - 1, room.GetCapacity());
        Assert.AreEqual(5, goblinBuilder.GetDamage());
        Assert.AreEqual(3, goblinBuilder.GetHealth());
    }

    [Test]
    public void UsingGoblinBuilderInRoomCapacityLessThan7_Entrance_ExpectRoomCapacityIs6Plus2AndStatIs5_3()
    {
        // Arrange
        room.SetCapacity(5);

        // Act
        var goblinBuilder = room.SpawnMonster(goblinBuilderInfo);

        // Assert
        Assert.AreEqual(7 - 1, room.GetCapacity());
        Assert.AreEqual(5, goblinBuilder.GetDamage());
        Assert.AreEqual(3, goblinBuilder.GetHealth());
    }

    [Test]
    public void UsingOrangeGraySlime_Entrance_ExpectStatIs2_3()
    {
        // Arrange

        // Act
        var orangeGraySlime = room.SpawnMonster(orangeGraySlimeInfo);

        // Assert
        Assert.AreEqual(4, orangeGraySlime.GetDamage());
        Assert.AreEqual(10, orangeGraySlime.GetHealth());
    }

    [Test]
    public void UsingGreenOrangeSlime_Exit_ExpectStatIs2_3()
    {
        // Arrange
        var greenOrangeSlime = room.SpawnMonster(greenOrangeSlimeInfo);

        // Act
        greenOrangeSlime.Exit();

        // Assert
        Assert.AreEqual(3, greenOrangeSlime.GetDamage());
        Assert.AreEqual(5, greenOrangeSlime.GetHealth());
    }


    [Test]
    public void UsingGoblinTailorSlime_Exit_ExpectStatIs1_2()
    {
        // Arrange
        room.SetCapacity(1);
        var goblinTailor = room.SpawnMonster(goblinTailorInfo);

        // Act
        goblinTailor.Exit();

        // Assert
        Assert.AreEqual(2, room.GetMaxCapacity());
        Assert.AreEqual(1, hand.GetSize());
        Assert.AreEqual(1, goblinTailor.GetDamage());
        Assert.AreEqual(2, goblinTailor.GetHealth());
    }
}
