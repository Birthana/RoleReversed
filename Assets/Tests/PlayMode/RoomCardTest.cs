using Moq;
using NUnit.Framework;
using System.Linq;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

public class RoomCardTest : MonoBehaviour
{
    [SetUp]
    public void Setup()
    {
    }

    [TearDown]
    public void TearDown()
    {
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
}
