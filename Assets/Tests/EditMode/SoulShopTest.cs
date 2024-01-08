using Moq;
using NUnit.Framework;
using System.Linq;
using UnityEngine;
using TMPro;

public class SoulShopTest : MonoBehaviour
{
    public Option option;
    public OptionInfo optionInfo;
    private SoulShop soulShop;
    private Mock<IMouseWrapper> mock;

    [SetUp]
    public void Setup()
    {
        option = TestHelper.GetOption();
        optionInfo = TestHelper.GetStarterPackOptionInfo();
        soulShop = new GameObject().AddComponent<SoulShop>();
        soulShop.optionPrefab = option;
        soulShop.optionInfos.Add(optionInfo);
        mock = new Mock<IMouseWrapper>(MockBehavior.Strict);
    }

    [TearDown]
    public void TearDown()
    {
        FindObjectsOfType<SoulShop>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
        FindObjectsOfType<Option>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
    }

    [Test]
    public void UsingSoulShop_IsOpen_ExpectFalse()
    {
        // Arrange
        var soulShop = new GameObject().AddComponent<SoulShop>();

        // Act
        var result = soulShop.IsOpen();

        // Assert
        Assert.AreEqual(false, result);
    }

    [Test]
    public void UsingSoulShop_Update_ExpectTrue()
    {
        // Arrange
        mock.Setup(x => x.PlayerPressesLeftClick()).Returns(true);
        mock.Setup(x => x.IsOnOpenSoulShop()).Returns(true);
        soulShop.SetMouseWrapper(mock.Object);

        // Act
        soulShop.Update();

        // Assert
        Assert.AreEqual(true, soulShop.IsOpen());
    }

    [Test]
    public void UsingSoulShop_Update_ExpectOptionCountIsThree()
    {
        // Arrange
        mock.Setup(x => x.PlayerPressesLeftClick()).Returns(true);
        mock.Setup(x => x.IsOnOpenSoulShop()).Returns(true);
        soulShop.SetMouseWrapper(mock.Object);

        // Act
        soulShop.Update();

        // Assert
        var expectedOptionCount = FindObjectsOfType<Option>();
        Assert.AreEqual(3 + 1, expectedOptionCount.Length);
    }

    [Test]
    public void UsingSoulShop_UpdateTwice_ExpectOptionCountIsOne()
    {
        // Arrange
        mock.Setup(x => x.PlayerPressesLeftClick()).Returns(true);
        mock.Setup(x => x.IsOnOpenSoulShop()).Returns(true);
        soulShop.SetMouseWrapper(mock.Object);
        soulShop.Update();

        // Act
        soulShop.Update();

        // Assert
        var expectedOptionCount = FindObjectsOfType<Option>();
        Assert.AreEqual(1, expectedOptionCount.Length);
    }

    [Test]
    public void UsingSoulShop_Update_ExpectOptionIsAtCorrectPosition()
    {
        // Arrange
        mock.Setup(x => x.PlayerPressesLeftClick()).Returns(true);
        mock.Setup(x => x.IsOnOpenSoulShop()).Returns(true);
        soulShop.SetMouseWrapper(mock.Object);
        soulShop.transform.position = new Vector3(3, 5);

        // Act
        soulShop.Update();

        // Assert
        var expectedOption = FindObjectsOfType<Option>();
        Assert.AreEqual(0.87f, TestHelper.RoundToNearestHundredth(expectedOption[0].transform.position.x));
    }

    [Test]
    public void UsingSoulShopThatIsOpen_Update_ExpectShopIsClosed()
    {
        // Arrange
        mock.Setup(x => x.PlayerPressesLeftClick()).Returns(true);
        mock.Setup(x => x.IsOnOpenSoulShop()).Returns(true);
        soulShop.SetMouseWrapper(mock.Object);
        soulShop.Update();

        // Act
        soulShop.Update();

        // Assert
        Assert.AreEqual(false, soulShop.IsOpen());
    }

    [Test]
    public void UsingSoulShopThatIsOpen_Update_ExpectOptionsAreInactive()
    {
        // Arrange
        mock.Setup(x => x.PlayerPressesLeftClick()).Returns(true);
        mock.Setup(x => x.IsOnOpenSoulShop()).Returns(true);
        soulShop.SetMouseWrapper(mock.Object);
        soulShop.Update();

        // Act
        var expectedOptions = FindObjectsOfType<Option>();
        soulShop.Update();

        // Assert
        Assert.AreEqual(false, expectedOptions[0].gameObject.activeSelf);
    }

    [Test]
    public void UsingSoulShopThatHasSpawnedOptions_Update_ExpectOptionsAreActive()
    {
        // Arrange
        mock.Setup(x => x.PlayerPressesLeftClick()).Returns(true);
        mock.Setup(x => x.IsOnOpenSoulShop()).Returns(true);
        soulShop.SetMouseWrapper(mock.Object);
        soulShop.Update();
        soulShop.Update();

        // Act
        soulShop.Update();

        // Assert
        var expectedOptions = FindObjectsOfType<Option>();
        Assert.AreEqual(3 + 1, expectedOptions.Length);
    }

    [Test]
    public void UsingSoulShop_Update_ExpectOptionInfo()
    {
        // Arrange
        mock.Setup(x => x.PlayerPressesLeftClick()).Returns(true);
        mock.Setup(x => x.IsOnOpenSoulShop()).Returns(true);
        soulShop.SetMouseWrapper(mock.Object);

        // Act
        soulShop.Update();

        // Assert
        var expectedOption = FindObjectOfType<Option>();
        Assert.AreEqual(optionInfo.cost, expectedOption.GetCost());
        Assert.AreEqual(optionInfo.description, expectedOption.GetDescription());
    }
}
