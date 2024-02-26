using Moq;
using NUnit.Framework;
using System.Linq;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

public class RoomCardTest : MonoBehaviour
{
    private Deck deck;

    [SetUp]
    public void Setup()
    {
        deck = TestHelper.GetDeck();
    }

    [TearDown]
    public void TearDown()
    {
        FindObjectsOfType<Deck>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
    }

    [UnityTest]
    public IEnumerator UsingSurprise_BattleStart_ExpectPlayerDamaged()
    {
        // Arrange
        var surpriseRoom = ScriptableObject.CreateInstance<SurpriseRoom>();
        var room = TestHelper.GetRoom();
        room.SetCardInfo(surpriseRoom);
        room.SpawnMonster(TestHelper.GetAnyMonsterCardInfo());
        var player = TestHelper.GetPlayer(0, 10);

        // Act
        yield return room.BattleStart();

        // Assert
        Assert.AreEqual(9, player.GetComponent<Health>().GetCount());
    }

    [UnityTest]
    public IEnumerator UsingSlimyRoom_BattleStart_ExpectTemporarySlimeSpawned()
    {
        // Arrange
        var slimyRoom = ScriptableObject.CreateInstance<SlimyRoom>();
        var temporarySlimeInfo = ScriptableObject.CreateInstance<TemporaryMonster>();
        temporarySlimeInfo.damage = 2;
        temporarySlimeInfo.health = 2;
        slimyRoom.tempMonsterCardInfo = temporarySlimeInfo;
        var room = TestHelper.GetRoom();
        room.SetCardInfo(slimyRoom);

        // Act
        yield return room.BattleStart();

        // Assert
        Assert.AreEqual(2, FindObjectOfType<TemporaryMonster>().damage);
        Assert.AreEqual(2, FindObjectOfType<TemporaryMonster>().health);
    }

    [UnityTest]
    public IEnumerator UsingOrangeRoom_BattleStart_ExpectEntrance()
    {
        // Arrange
        var orangeRoom = ScriptableObject.CreateInstance<OrangeRoom>();
        var goblinBuilderInfo = ScriptableObject.CreateInstance<GoblinBuilder>();
        goblinBuilderInfo.damage = 2;
        goblinBuilderInfo.health = 2;
        var room = TestHelper.GetRoom();
        room.SetCardInfo(orangeRoom);
        room.SpawnMonster(goblinBuilderInfo);

        // Act
        yield return room.BattleStart();

        // Assert
        Assert.AreEqual(5, room.GetCapacity());
    }
}
