using Moq;
using NUnit.Framework;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillManagerTest : MonoBehaviour
{
    public SkillInfo slashInfo;
    public SkillInfo decayInfo;

    [SetUp]
    public void Setup()
    {
        slashInfo = ScriptableObject.CreateInstance<Slash>();
        decayInfo = ScriptableObject.CreateInstance<Decay>();
    }

    [TearDown]
    public void TearDown()
    {
    }

    [Test]
    public void UsingPlayerSkillManager_GetNumberOfSkills_ExpectZero()
    {
        // Arrange
        var playerSkillManager = new GameObject().AddComponent<PlayerSkillManager>();

        // Act
        var count = playerSkillManager.GetNumberOfSkills();

        // Assert
        Assert.AreEqual(0, count);
    }

    [Test]
    public void UsingPlayerSkillManager_GetNumberOfSkills_ExpectOne()
    {
        // Arrange
        var playerSkillManager = new GameObject().AddComponent<PlayerSkillManager>();
        var playerSkill = new PlayerSkill(2, slashInfo);
        playerSkillManager.Add(playerSkill);

        // Act
        var count = playerSkillManager.GetNumberOfSkills();

        // Assert
        Assert.AreEqual(1, count);
    }

    [Test]
    public void UsingPlayerSkillManager_ReduceSkills_ExpectOne()
    {
        // Arrange
        var playerSkillManager = new GameObject().AddComponent<PlayerSkillManager>();
        var playerSkill = new PlayerSkill(2, slashInfo);
        playerSkillManager.Add(playerSkill);
        var player = TestHelper.GetPlayerInRoom(1, 2);

        // Act
        playerSkillManager.ReduceSkills(1);

        // Assert
        Assert.AreEqual(1, playerSkillManager.skills[0].GetTimer());
    }

    [Test]
    public void UsingPlayerSkillManager_ReduceSkillsToZero_ExpectOne()
    {
        // Arrange
        var playerSkillManager = new GameObject().AddComponent<PlayerSkillManager>();
        var playerSkill = new PlayerSkill(2, slashInfo);
        playerSkillManager.Add(playerSkill);
        var player = TestHelper.GetPlayerInRoom(1, 2);
        player.transform.parent.GetComponent<Room>().SpawnMonster(TestHelper.GetAnyMonsterCardInfo());

        // Act
        playerSkillManager.ReduceSkills(2);

        // Assert
        Assert.AreEqual(2, playerSkillManager.skills[0].GetTimer());
        Assert.AreEqual(2, player.GetComponent<Health>().GetCurrentHealth());
        Assert.AreEqual(0, player.transform.parent.GetComponentInChildren<Monster>().GetComponent<Health>().GetCurrentHealth());
    }

    [Test]
    public void UsingPlayerSkillManagerWithDecay_ReduceSkillsToZero_ExpectOne()
    {
        // Arrange
        var playerSkillManager = new GameObject().AddComponent<PlayerSkillManager>();
        var playerSkill = new PlayerSkill(2, decayInfo);
        playerSkillManager.Add(playerSkill);
        var player = TestHelper.GetPlayerInRoom(1, 2);
        player.transform.parent.GetComponent<Room>().SpawnMonster(TestHelper.GetAnyMonsterCardInfo());

        // Act
        playerSkillManager.ReduceSkills(2);

        // Assert
        Assert.AreEqual(2, playerSkillManager.skills[0].GetTimer());
        Assert.AreEqual(2, player.GetComponent<Health>().GetCurrentHealth());
        Assert.AreEqual(-1, player.transform.parent.GetComponentInChildren<Monster>().GetComponent<Health>().GetCurrentHealth());
    }
}
