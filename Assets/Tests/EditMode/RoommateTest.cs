using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class RoommateTest : MonoBehaviour
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
    public void UsingNoMonsters_CreateNewRoommates_ExpectGivenInput()
    {
        // Arrange
        List<Monster> monster = new List<Monster>();

        // Acts
        var roomates = new Roommates(monster);

        // Assert
        Assert.AreEqual(monster, roomates.Get());
    }

    [Test]
    public void UsingNoMonsters_GetRequest_ExpectNoRequest()
    {
        // Arrange
        List<Monster> monster = new List<Monster>();
        var roomates = new Roommates(monster);

        // Acts
        var request = roomates.CreateRequest();

        // Assert
        Assert.AreEqual(monster, request);
        Assert.AreEqual(0, request.Count);
    }

    [Test]
    public void UsingMonsters_GetRequest_ExpectNotTheSameMonsters()
    {
        // Arrange
        List<Monster> monsters = new List<Monster>();
        var monster1 = new GameObject().AddComponent<Monster>();
        monster1.gameObject.AddComponent<SpriteRenderer>();
        monster1.gameObject.AddComponent<Damage>();
        monster1.gameObject.AddComponent<Health>();
        monster1.Setup(TestHelper.GetAnyMonsterCardInfo());
        var room1 = TestHelper.GetRoom();
        room1.transform.position = new Vector3(2, 5, 0);
        monster1.transform.SetParent(room1.transform);
        var monster2 = new GameObject().AddComponent<Monster>();
        monster2.gameObject.AddComponent<SpriteRenderer>();
        monster2.gameObject.AddComponent<Damage>();
        monster2.gameObject.AddComponent<Health>();
        monster2.Setup(TestHelper.GetAnyOtherMonsterCardInfo());
        var room2 = TestHelper.GetRoom();
        monster2.transform.SetParent(room2.transform);
        monsters.Add(monster1);
        monsters.Add(monster2);
        var roomates = new Roommates(monsters);

        // Acts
        var request = roomates.CreateRequest();

        // Assert
        Assert.AreEqual(2, request.Count);
    }
}
