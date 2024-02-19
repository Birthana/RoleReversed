using Moq;
using NUnit.Framework;
using System.Linq;
using UnityEngine;
using TMPro;

public class DraftManagerTest : MonoBehaviour
{
    private Mock<IMouseWrapper> mock;
    private DraftManager draftManager;
    private CardManager cardManager;
    private CardDragger cardDragger;
    private Deck deck;
    private static readonly int ANY_SPACING = 10;

    [SetUp]
    public void Setup()
    {
        mock = new Mock<IMouseWrapper>(MockBehavior.Strict);
        draftManager = TestHelper.GetDraftManager(ANY_SPACING);
        cardManager = TestHelper.GetCardManager();
        cardDragger = TestHelper.GetCardDragger();
        deck = TestHelper.GetDeck();
    }

    [TearDown]
    public void TearDown()
    {
        FindObjectsOfType<DraftCard>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
        FindObjectsOfType<CardManager>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
    }

    [Test]
    public void UsingDraftManager_Draft_ExpectCardInfoCountIs3()
    {
        // Arrange

        // Act
        draftManager.Draft();

        // Assert
        Assert.AreEqual(3, draftManager.GetCount());
    }

    [Test]
    public void UsingDraftManager_Draft_ExpectDraftCards()
    {
        // Arrange

        // Act
        draftManager.Draft();

        // Assert
        var draftCards = FindObjectsOfType<DraftCard>();
        Assert.AreEqual(3, draftCards.Length);
    }

    [Test]
    public void UsingDraftManager_Draft_ExpectDraftCardsAreSpreadOut()
    {
        // Arrange

        // Act
        draftManager.Draft();

        // Assert
        var draftCards = FindObjectsOfType<DraftCard>();
        Assert.AreEqual(1.75f, TestHelper.RoundToNearestHundredth(draftCards[0].transform.localPosition.x));
        Assert.AreEqual(0, draftCards[1].transform.localPosition.x);
        Assert.AreEqual(-1.75f, TestHelper.RoundToNearestHundredth(draftCards[2].transform.localPosition.x));
    }

    [Test]
    public void UsingDraftManager_Draft_ExpectDraftCardAddedToHand()
    {
        // Arrange
        var draftCard = TestHelper.GetRoomDraftCard();
        mock.Setup(x => x.PlayerPressesLeftClick()).Returns(true);
        mock.Setup(x => x.IsOnDraft()).Returns(true);
        mock.Setup(x => x.GetHitComponent<DraftCard>()).Returns(draftCard);
        draftManager.SetMouseWrapper(mock.Object);

        // Act
        draftManager.Update();

        // Assert
        var draftCards = FindObjectsOfType<DraftCard>();
        Assert.AreEqual(0, draftCards.Length);
    }

    [Test]
    public void UsingDraftManager_Draft_ExpectDraftCardsAreUnique()
    {
        // Arrange

        // Act
        draftManager.Draft();

        // Assert
        var draftCards = FindObjectsOfType<DraftCard>();
        Assert.AreEqual(false, draftCards[0].GetCardInfo().Equals(draftCards[1].GetCardInfo()));
        Assert.AreEqual(false, draftCards[1].GetCardInfo().Equals(draftCards[2].GetCardInfo()));
        Assert.AreEqual(false, draftCards[0].GetCardInfo().Equals(draftCards[2].GetCardInfo()));
    }

}
