using Moq;
using System.Collections;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


public class PackAnimationTest : MonoBehaviour
{
    private Mock<IMouseWrapper> mock;
    private Mock<ICardDragger> cardDraggerMock;
    private CardManager cardManager;
    private CardDragger cardDragger;
    private Hand hand;
    private Deck deck;

    [SetUp]
    public void Setup()
    {
        cardManager = TestHelper.GetCardManager();
        hand = TestHelper.GetHand();
        deck = TestHelper.GetDeck();
        mock = new Mock<IMouseWrapper>(MockBehavior.Strict);
        cardDraggerMock = new Mock<ICardDragger>(MockBehavior.Strict);
        cardDraggerMock.Setup(x => x.UpdateLoop());
        cardDraggerMock.Setup(x => x.ResetHover());
        hand.SetCardDragger(cardDraggerMock.Object);
    }

    [TearDown]
    public void TearDown()
    {
        FindObjectsOfType<Deck>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
        FindObjectsOfType<Hand>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
    }

    [UnityTest]
    public IEnumerator UsingPack_ClickOnPack_Expect2CardsInHand()
    {
        // Arrange
        var pack = TestHelper.GetPack();
        mock.Setup(x => x.PlayerPressesLeftClick()).Returns(true);
        mock.Setup(x => x.IsOnPack()).Returns(true);
        mock.Setup(x => x.GetHitComponent<Pack>()).Returns(pack);
        pack.SetMouseWrapper(mock.Object);
        pack.LoadStarterPack();

        // Act
        pack.Update();
        yield return new WaitForSeconds(LootAnimation.ANIMATION_TIME);

        // Assert
        Assert.AreEqual(2, hand.hand.Count);
    }

    [UnityTest]
    public IEnumerator UsingTwoPacks_ClickOnPack_Expect2CardsInHand()
    {
        // Arrange
        var pack1 = TestHelper.GetPack();
        var pack2 = TestHelper.GetPack();
        mock.Setup(x => x.PlayerPressesLeftClick()).Returns(true);
        mock.Setup(x => x.IsOnPack()).Returns(true);
        mock.Setup(x => x.GetHitComponent<Pack>()).Returns(pack1);
        pack1.SetMouseWrapper(mock.Object);
        pack1.LoadStarterPack();
        pack2.SetMouseWrapper(mock.Object);
        pack2.LoadStarterPack();

        // Act
        pack1.Update();
        pack2.Update();
        yield return new WaitForSeconds(LootAnimation.ANIMATION_TIME);

        // Assert
        Assert.AreEqual(2, hand.hand.Count);
    }
}
