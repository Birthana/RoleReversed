using Moq;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerTest : MonoBehaviour
{
    private CardManager cardManager;
    private GameManager gameManager;
    private Mock<IFocusAnimation> mock;

    [SetUp]
    public void Setup()
    {
        mock = new Mock<IFocusAnimation>(MockBehavior.Strict);
        mock.Setup(x => x.SetFocusPosition(Vector3.up * 3));
        mock.Setup(x => x.SetFocusScale(2.5f));
        cardManager = TestHelper.GetCardManager();
        gameManager = TestHelper.GetGameManager();
    }

    [TearDown]
    public void TearDown()
    {
        FindObjectsOfType<GameManager>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
        FindObjectsOfType<CardManager>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
    }

    [Test]
    public void UsingGameManager_Awake_ExpectPack()
    {
        // Arrange
        gameManager.SetFocusAnimation(mock.Object);

        // Act
        gameManager.Awake();

        // Assert
        var pack = FindObjectsOfType<Pack>();
        Assert.AreEqual(1 + 1, pack.Length);
    }

    [Test]
    public void UsingGameManagerWithShopButton_Awake_ExpectOff()
    {
        // Arrange
        gameManager.SetFocusAnimation(mock.Object);
        var shopButton = new GameObject();
        gameManager.shopButton = shopButton;

        // Act
        gameManager.Awake();

        // Assert
        Assert.AreEqual(false, shopButton.activeSelf);
    }

    [Test]
    public void UsingGameManagerWithStartButton_Awake_ExpectOff()
    {
        // Arrange
        gameManager.SetFocusAnimation(mock.Object);
        var startButton = new GameObject();
        gameManager.startButton = startButton;

        // Act
        gameManager.Awake();

        // Assert
        Assert.AreEqual(false, startButton.activeSelf);
    }

    [Test]
    public void UsingGameManagerWithShopButton_EnableShopButton_ExpectOn()
    {
        // Arrange
        gameManager.SetFocusAnimation(mock.Object);
        var shopButton = new GameObject();
        gameManager.shopButton = shopButton;

        // Act
        gameManager.EnableShopButton();

        // Assert
        Assert.AreEqual(true, shopButton.activeSelf);
    }

    [Test]
    public void UsingGameManagerWithStartButton_EnableStartButton_ExpectOn()
    {
        // Arrange
        gameManager.SetFocusAnimation(mock.Object);
        var startButton = new GameObject();
        gameManager.startButton = startButton;

        // Act
        gameManager.EnableStartButton();

        // Assert
        Assert.AreEqual(true, startButton.activeSelf);
    }

    [Test]
    public void UsingGameManager_EnableStartButton_ExpectOn()
    {
        // Arrange
        gameManager.SetFocusAnimation(mock.Object);
        var startButton = new GameObject();
        gameManager.startButton = startButton;
        startButton.SetActive(false);
        var room = new GameObject().AddComponent<Room>();

        // Act
        gameManager.SetStartRoom(room);

        // Assert
        Assert.AreEqual(true, startButton.activeSelf);
    }
}
