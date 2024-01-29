using Moq;
using NUnit.Framework;
using UnityEngine;

public class PackTest
{
    private Mock<IMouseWrapper> mock;
    private CardManager cardManager;
    private Hand hand;
    private CardDragger cardDragger;
    private Deck deck;

    [SetUp]
    public void Setup()
    {
        cardManager = TestHelper.GetCardManager();
        hand = TestHelper.GetHand();
        cardDragger = TestHelper.GetCardDragger();
        deck = TestHelper.GetDeck();
        mock = new Mock<IMouseWrapper>(MockBehavior.Strict);
    }

    [Test]
    public void UsingPack_CreateStarterPack_Expect2Cards()
    {
        // Arrange
        var pack = TestHelper.GetPack();

        // Act
        pack.CreateStarterPack();

        // Assert
        Assert.AreEqual(2, pack.GetSize());
    }

    [Test]
    public void UsingPack_CreateStarterPack_ExpectPackCostIsLessThanThree()
    {
        // Arrange
        var pack = TestHelper.GetPack();

        // Act
        pack.CreateStarterPack();

        // Assert
        Assert.AreEqual(true, pack.GetTotalCost() <= 3);
    }

    [Test]
    public void UsingPack_ClickOnPack_ExpectCardsAre3OrLess()
    {
        // Arrange
        var pack = TestHelper.GetPack();
        mock.Setup(x => x.PlayerPressesLeftClick()).Returns(true);
        mock.Setup(x => x.IsOnPack()).Returns(true);
        mock.Setup(x => x.GetHitComponent<Pack>()).Returns(pack);
        pack.SetMouseWrapper(mock.Object);

        // Act
        pack.Update();

        // Assert
        Assert.AreEqual(true, pack.GetTotalCost() <= 3);
    }
}
