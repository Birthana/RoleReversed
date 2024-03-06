using Moq;
using System.Collections;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestSetup
{
    private Mock<IMouseWrapper> mock;

    public TestSetup()
    {
        mock = new Mock<IMouseWrapper>(MockBehavior.Strict);
    }

    public Mock<IMouseWrapper> Get()
    {
        return mock;
    }

    public TestSetup WithPlayerSelectsCard(Card card)
    {
        mock.Setup(x => x.PlayerPressesLeftClick()).Returns(true);
        mock.Setup(x => x.IsOnHand()).Returns(true);
        mock.Setup(x => x.GetHitComponent<Card>()).Returns(card);
        return this;
    }

    public TestSetup WithPlayerDoesNotSelectCard()
    {
        mock.Setup(x => x.PlayerPressesLeftClick()).Returns(false);
        return this;
    }

    public TestSetup WithPlayerHoversCard(Card card)
    {
        mock.Setup(x => x.IsOnHand()).Returns(true);
        mock.Setup(x => x.GetHitComponent<Card>()).Returns(card);
        return this;
    }
}

public class CardDraggerTest : MonoBehaviour
{
    private Hand hand;

    [SetUp]
    public void Setup()
    {
        hand = TestHelper.GetHand();
    }

    [TearDown]
    public void TearDown()
    {
        FindObjectsOfType<Hand>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
        FindObjectsOfType<Card>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
        FindObjectsOfType<CardDragger>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
    }

    [UnityTest]
    public IEnumerator GivenNoSelectedCards_Update_ExpectNoErrors()
    {
        // Arrange
        var card = new GameObject().AddComponent<Card>();
        var draftManager = TestHelper.GetDraftManager(2);
        var mock = new TestSetup().WithPlayerDoesNotSelectCard().WithPlayerHoversCard(card).Get();
        var cardDragger = TestHelper.GetCardDragger();
        cardDragger.SetMouseWrapper(mock.Object);

        // Act
        cardDragger.UpdateLoop();
        yield return new WaitForEndOfFrame();

        // Assert
        mock.VerifyAll();
    }
}
