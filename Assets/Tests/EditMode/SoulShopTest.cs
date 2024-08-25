using Moq;
using NUnit.Framework;
using System.Linq;
using UnityEngine;
using TMPro;

public class SoulShopTest : MonoBehaviour
{
    public Option option;
    public StarterPack optionInfo;
    private SoulShop soulShop;
    private Mock<IMouseWrapper> mock;

    [SetUp]
    public void Setup()
    {
        option = TestHelper.GetOption();
        optionInfo = TestHelper.GetStarterPackOptionInfo();
        soulShop = TestHelper.GetSoulShop(option, optionInfo);
        mock = new Mock<IMouseWrapper>(MockBehavior.Strict);
    }

    [TearDown]
    public void TearDown()
    {
        FindObjectsOfType<SoulShop>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
        FindObjectsOfType<Option>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
    }

    public void SetMocks()
    {
        mock.Setup(x => x.PlayerPressesLeftClick()).Returns(true);
        mock.Setup(x => x.IsOnOpenSoulShop()).Returns(true);
        soulShop.SetMouseWrapper(mock.Object);
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
        SetMocks();

        // Act
        soulShop.Update();

        // Assert
        Assert.AreEqual(true, soulShop.IsOpen());
    }

    [Test]
    public void UsingSoulShop_Update_ExpectOptionCountIsThree()
    {
        // Arrange
        SetMocks();

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
        SetMocks();
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
        SetMocks();
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
        SetMocks();
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
        SetMocks();
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
        SetMocks();
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
        SetMocks();

        // Act
        soulShop.Update();

        // Assert
        var expectedOption = FindObjectOfType<Option>();
        Assert.AreEqual(optionInfo.cost, expectedOption.GetCost());
        Assert.AreEqual(optionInfo.description, expectedOption.GetDescription());
    }

    [Test]
    public void UsingSoulShop_Update_ExpectDifferentOptionInfos()
    {
        // Arrange
        SetMocks();

        // Act
        soulShop.Update();

        // Assert
        var expectedOptions = FindObjectsOfType<Option>();
        Assert.AreEqual(4, expectedOptions.Length);
    }
}
