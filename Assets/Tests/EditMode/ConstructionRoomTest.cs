using Moq;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionRoomTest : MonoBehaviour
{
    private Hand hand;
    private Mock<IMouseWrapper> mock;
    private Mock<ICardDragger> cardDraggerMock;
    private CardManager cardManager;

    [SetUp]
    public void Setup()
    {
        hand = TestHelper.GetHand();
        mock = new Mock<IMouseWrapper>(MockBehavior.Strict);
        cardDraggerMock = new Mock<ICardDragger>(MockBehavior.Strict);
        cardDraggerMock.Setup(x => x.UpdateLoop());
        cardDraggerMock.Setup(x => x.ResetHover());
        hand.SetCardDragger(cardDraggerMock.Object);
        cardManager = TestHelper.GetCardManager();
    }

    [TearDown]
    public void TearDown()
    {
        FindObjectsOfType<ConstructionRoom>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
        FindObjectsOfType<Room>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
        FindObjectsOfType<CardManager>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
    }

    [Test]
    public void UsingConstructionRoomCardInfo_Cast_ExpectConstructionRoomSpawn()
    {
        // Arrange
        var roomCard = TestHelper.GetRoomCard();
        mock.Setup(x => x.GetHitTransform()).Returns(new GameObject().transform);
        roomCard.SetMouseWrapper(mock.Object);
        roomCard.SetCardInfo(TestHelper.GetConstructionRoomCardInfo());
        roomCard.SetRoomTransform();

        // Act
        roomCard.Cast();

        // Assert
        var constructionRooms = FindObjectsOfType<ConstructionRoom>();
        Assert.AreEqual(1, constructionRooms.Length);
    }

    [Test]
    public void UsingConstructionRoomCardInfo_Cast_ExpectTimer()
    {
        // Arrange
        var roomCard = TestHelper.GetRoomCard();
        mock.Setup(x => x.GetHitTransform()).Returns(new GameObject().transform);
        roomCard.SetMouseWrapper(mock.Object);
        roomCard.SetCardInfo(TestHelper.GetConstructionRoomCardInfo());
        roomCard.SetRoomTransform();

        // Act
        roomCard.Cast();

        // Assert
        var constructionRoom = FindObjectOfType<ConstructionRoom>();
        Assert.AreEqual(2, ((ConstructionRoomCardInfo)constructionRoom.GetCardInfo()).GetTimer());
    }

    [Test]
    public void UsingConstructionRoomCardInfo_Cast_ExpectRoomTimer()
    {
        // Arrange
        var roomCard = TestHelper.GetRoomCard();
        mock.Setup(x => x.GetHitTransform()).Returns(new GameObject().transform);
        roomCard.SetMouseWrapper(mock.Object);
        roomCard.SetCardInfo(TestHelper.GetConstructionRoomCardInfo());
        roomCard.SetRoomTransform();

        // Act
        roomCard.Cast();

        // Assert
        var constructionRoom = FindObjectOfType<ConstructionRoom>();
        Assert.AreEqual(2, constructionRoom.GetComponent<Timer>().GetCount());
    }

    [Test]
    public void UsingConstructionRoomCardInfoWithAMonster_ReduceTimer_ExpectRoomTimerIs1()
    {
        // Arrange
        var roomCard = TestHelper.GetRoomCard();
        mock.Setup(x => x.GetHitTransform()).Returns(new GameObject().transform);
        roomCard.SetMouseWrapper(mock.Object);
        roomCard.SetCardInfo(TestHelper.GetConstructionRoomCardInfo());
        roomCard.SetRoomTransform();
        roomCard.Cast();
        var constructionRoom = FindObjectOfType<ConstructionRoom>();
        constructionRoom.SpawnMonster(TestHelper.GetAnyMonsterCardInfo());

        // Act
        constructionRoom.ReduceTimer();

        // Assert
        Assert.AreEqual(1, constructionRoom.GetComponent<Timer>().GetCount());
    }

    [Test]
    public void UsingConstructionRoomCardInfo_ReduceTimer_ExpectRoomTimerIs2()
    {
        // Arrange
        var roomCard = TestHelper.GetRoomCard();
        mock.Setup(x => x.GetHitTransform()).Returns(new GameObject().transform);
        roomCard.SetMouseWrapper(mock.Object);
        roomCard.SetCardInfo(TestHelper.GetConstructionRoomCardInfo());
        roomCard.SetRoomTransform();
        roomCard.Cast();
        var constructionRoom = FindObjectOfType<ConstructionRoom>();

        // Act
        constructionRoom.ReduceTimer();

        // Assert
        Assert.AreEqual(2, constructionRoom.GetComponent<Timer>().GetCount());
    }

    [Test]
    public void UsingConstructionRoomCardInfo_SpawnRoom_ExpectRoomIsSpawned()
    {
        // Arrange
        var roomCard = TestHelper.GetRoomCard();
        mock.Setup(x => x.GetHitTransform()).Returns(new GameObject().transform);
        roomCard.SetMouseWrapper(mock.Object);
        roomCard.SetCardInfo(TestHelper.GetConstructionRoomCardInfo());
        roomCard.SetRoomTransform();
        roomCard.Cast();
        var constructionRoom = FindObjectOfType<ConstructionRoom>();
        constructionRoom.transform.position = new Vector3(3, 5, 0);

        // Act
        constructionRoom.SpawnRoom();

        // Assert
        var rooms = FindObjectsOfType<Room>();
        Assert.AreEqual(1, rooms.Length);
        Assert.AreEqual(new Vector3(3, 5, 0), rooms[0].transform.position);
        Assert.AreEqual(TestHelper.GetRoomCardInfo(2, "Any Name.", "Any Effect."), rooms[0].GetCardInfo());
    }

    [Test]
    public void UsingConstructionRoomCardInfo_SpawnRoom_ExpectRoomHasChildren()
    {
        // Arrange
        var roomCard = TestHelper.GetRoomCard();
        mock.Setup(x => x.GetHitTransform()).Returns(new GameObject().transform);
        roomCard.SetMouseWrapper(mock.Object);
        roomCard.SetCardInfo(TestHelper.GetConstructionRoomCardInfo());
        roomCard.SetRoomTransform();
        roomCard.Cast();
        var constructionRoom = FindObjectOfType<ConstructionRoom>();
        constructionRoom.transform.position = new Vector3(3, 5, 0);
        var monsterCardInfo = TestHelper.GetAnyMonsterCardInfo();
        constructionRoom.SpawnMonster(monsterCardInfo);

        // Act
        constructionRoom.SpawnRoom();

        // Assert
        var rooms = FindObjectsOfType<Room>();
        Assert.AreEqual(1, rooms.Length);
        Assert.AreEqual(monsterCardInfo, rooms[0].GetRandomMonster().cardInfo);
    }
}
