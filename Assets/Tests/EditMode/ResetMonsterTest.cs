using Moq;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ResetMonsterTest : MonoBehaviour
{
    [SetUp]
    public void Setup()
    {
    }

    [TearDown]
    public void TearDown()
    {
    }

    [Test]
    public void XXX()
    {
        // Arrange
        var resetMonster = new GameObject().AddComponent<ResetMonster>();
        var room = TestHelper.GetRoom();
        room.SpawnTemporaryMonster(TestHelper.GetTemporaryMonsterCardInfo());

        // Act
        resetMonster.DestroyAllTempMonsters();

        // Assert
        Assert.AreEqual(0, resetMonster.GetMonsters().Count);
    }
}
