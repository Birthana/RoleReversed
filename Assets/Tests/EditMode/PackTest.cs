using Moq;
using NUnit.Framework;
using UnityEngine;

public class PackTest
{
    private Mock<IMouseWrapper> mock;
    private CardManager cardManager;
    private Hand hand;
    private CardDragger cardDragger;

    [SetUp]
    public void Setup()
    {
        cardManager = TestHelper.GetCardManager();
        hand = TestHelper.GetHand();
        cardDragger = TestHelper.GetCardDragger();
        mock = new Mock<IMouseWrapper>(MockBehavior.Strict);
    }

    [Test]
    public void UsingPack_CreateRarityPack_Expect5Cards()
    {
        // Arrange
        var pack = new GameObject().AddComponent<Pack>();

        // Act
        pack.CreateRarityPack();

        // Assert
        Assert.AreEqual(5, pack.GetSize());
    }

    [Test]
    public void UsingPack_CreateStarterPack_Expect2Cards()
    {
        // Arrange
        var pack = new GameObject().AddComponent<Pack>();

        // Act
        pack.CreateStarterPack();

        // Assert
        Assert.AreEqual(2, pack.GetSize());
    }

    [Test]
    public void UsingPack_CreateStarterPack_ExpectPackCostIsLessThanThree()
    {
        // Arrange
        var pack = new GameObject().AddComponent<Pack>();

        // Act
        pack.CreateStarterPack();

        // Assert
        Assert.AreEqual(true, pack.GetTotalCost() <= 3);
    }

    [Test]
    public void UsingPack_ClickOnPack_Expect2CardsInHand()
    {
        // Arrange
        var pack = new GameObject().AddComponent<Pack>();
        mock.Setup(x => x.PlayerPressesLeftClick()).Returns(true);
        mock.Setup(x => x.IsOnPack()).Returns(true);
        pack.SetMouseWrapper(mock.Object);

        // Act
        pack.Update();

        // Assert
        Assert.AreEqual(2, hand.hand.Count);
    }

    [Test]
    public void UsingPack_ClickOnPack_ExpectCardsAre3OrLess()
    {
        // Arrange
        var pack = new GameObject().AddComponent<Pack>();
        mock.Setup(x => x.PlayerPressesLeftClick()).Returns(true);
        mock.Setup(x => x.IsOnPack()).Returns(true);
        pack.SetMouseWrapper(mock.Object);

        // Act
        pack.Update();

        // Assert
        Assert.AreEqual(true, pack.GetTotalCost() <= 3);
    }
}
