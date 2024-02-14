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
    public IEnumerator XXX()
    {
        // Arrange
        var surpriseRoom = ScriptableObject.CreateInstance<SurpriseRoom>();
        var room = TestHelper.GetRoom();
        room.SetCardInfo(surpriseRoom);
        room.SpawnMonster(TestHelper.GetAnyMonsterCardInfo());

        // Act
        yield return room.BattleStart();

        // Assert
        Assert.AreEqual(false, true);
    }
}
