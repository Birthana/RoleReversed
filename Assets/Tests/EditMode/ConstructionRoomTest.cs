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

    [Test]
    public void UsingConstructionRoomCardInfo_Cast_ExpectConstructionRoomSpawn()
    {
        // Arrange
        var roomCard = TestHelper.GetRoomCard();
        mock.Setup(x => x.GetHitTransform()).Returns(new GameObject().transform);
        roomCard.SetMouseWrapper(mock.Object);
        var constructionRoomCardInfo = ScriptableObject.CreateInstance<ConstructionRoomCardInfo>();
        constructionRoomCardInfo.cardName = "Any Name.";
        constructionRoomCardInfo.cost = 2;
        constructionRoomCardInfo.effectDescription = "Any Effect.";
        roomCard.SetCardInfo(constructionRoomCardInfo);

        // Act
        roomCard.Cast();

        // Assert
        var constructionRooms = FindObjectsOfType<ConstructionRoom>();
        Assert.AreEqual(1, constructionRooms.Length);
    }
}
