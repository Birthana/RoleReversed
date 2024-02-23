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

    [SetUp]
    public void Setup()
    {
        hand = TestHelper.GetHand();
        mock = new Mock<IMouseWrapper>(MockBehavior.Strict);
        cardDraggerMock = new Mock<ICardDragger>(MockBehavior.Strict);
        cardDraggerMock.Setup(x => x.UpdateLoop());
        cardDraggerMock.Setup(x => x.ResetHover());
        hand.SetCardDragger(cardDraggerMock.Object);
    }

    [TearDown]
    public void TearDown()
    {
    }

    //[Test]
    //public void UsingConstructionRoomCardInfo_Cast_ExpectConstructionRoomSpawn()
    //{
    //    // Arrange
    //    var roomCard = TestHelper.GetRoomCard();
    //    var randomTransform = new GameObject();
    //    mock.Setup(x => x.GetHitTransform()).Returns(randomTransform.transform);
    //    roomCard.SetMouseWrapper(mock.Object);
    //    roomCard.SetCardInfo(TestHelper.GetRoomCardInfo(1, "Name", "Effect"));

    //    // Act
    //    roomCard.Cast();

    //    // Assert
    //    var constructionRooms = FindObjectsOfType<ConstructionRoom>();
    //    Assert.AreEqual(1, constructionRooms.Length);
    //}
}
