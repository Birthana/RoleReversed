using Moq;
using NUnit.Framework;
using System.Linq;
using UnityEngine;
using TMPro;

public class OptionTest : MonoBehaviour
{
    private CardManager cardManager;
    private SoulShop soulShop;
    private PlayerSoulCounter playerSoulCount;
    private Option option;
    private OptionInfo optionInfo;
    private Mock<IMouseWrapper> mock;

    [SetUp]
    public void Setup()
    {
        cardManager = TestHelper.GetCardManager();
        soulShop = new GameObject().AddComponent<SoulShop>();
        playerSoulCount = new GameObject().AddComponent<PlayerSoulCounter>();
        playerSoulCount.IncreaseSouls();
        option = TestHelper.GetOption();
        optionInfo = TestHelper.GetStarterPackOptionInfo();
        mock = new Mock<IMouseWrapper>(MockBehavior.Strict);
    }

    [TearDown]
    public void TearDown()
    {
        FindObjectsOfType<Option>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
        FindObjectsOfType<Pack>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
    }

    [Test]
    public void UsingStarterPackOption_ClickOnOption_Expect1PackSpawn()
    {
        // Arrange
        option.SetOptionInfo(optionInfo);
        mock.Setup(x => x.PlayerPressesLeftClick()).Returns(true);
        mock.Setup(x => x.IsOnOption()).Returns(true);
        mock.Setup(x => x.GetHitComponent<Option>()).Returns(option);
        option.SetMouseWrapper(mock.Object);
        soulShop.optionPrefab = option;
        soulShop.optionInfos.Add(optionInfo);

        // Act
        option.Update();

        // Assert
        var expectedPacks = FindObjectsOfType<Pack>();
        Assert.AreEqual(1 + 1, expectedPacks.Length);
    }

    [Test]
    public void UsingTwoOptions_ClickOnOption_Expect1PackSpawn()
    {
        // Arrange
        option.SetOptionInfo(optionInfo);
        mock.Setup(x => x.PlayerPressesLeftClick()).Returns(true);
        mock.Setup(x => x.IsOnOption()).Returns(true);
        mock.Setup(x => x.GetHitComponent<Option>()).Returns(option);
        option.SetMouseWrapper(mock.Object);
        var option2 = TestHelper.GetOption();
        option2.SetOptionInfo(optionInfo);
        option2.SetMouseWrapper(mock.Object);
        soulShop.optionPrefab = option;
        soulShop.optionInfos.Add(optionInfo);

        // Act
        option.Update();
        option2.Update();

        // Assert
        var expectedPacks = FindObjectsOfType<Pack>();
        Assert.AreEqual(1 + 1, expectedPacks.Length);
    }

    [Test]
    public void UsingStarterPackOption_ClickOnOption_ExpectPlayerSoulCountDownBy1()
    {
        // Arrange
        option.SetOptionInfo(optionInfo);
        mock.Setup(x => x.PlayerPressesLeftClick()).Returns(true);
        mock.Setup(x => x.IsOnOption()).Returns(true);
        mock.Setup(x => x.GetHitComponent<Option>()).Returns(option);
        option.SetMouseWrapper(mock.Object);
        soulShop.optionPrefab = option;
        soulShop.optionInfos.Add(optionInfo);

        // Act
        option.Update();

        // Assert
        var expectedSoulCount = FindObjectOfType<PlayerSoulCounter>();
        Assert.AreEqual(0, expectedSoulCount.GetCurrentSouls());
    }

    [Test]
    public void UsingStarterPackOptionWithSoulShopIsOpen_ClickOnOption_ExpectSoulShopIsClosed()
    {
        // Arrange
        option.SetOptionInfo(optionInfo);
        mock.Setup(x => x.PlayerPressesLeftClick()).Returns(true);
        mock.Setup(x => x.IsOnOption()).Returns(true);
        mock.Setup(x => x.GetHitComponent<Option>()).Returns(option);
        option.SetMouseWrapper(mock.Object);
        soulShop.optionPrefab = option;
        soulShop.optionInfos.Add(optionInfo);
        soulShop.OpenShop();

        // Act
        option.Update();

        // Assert
        var expectedSoulShop = FindObjectOfType<SoulShop>();
        Assert.AreEqual(false, expectedSoulShop.IsOpen());
    }

    [Test]
    public void UsingStarterPackOption_ClickOnOption_ExpectPackIsNotSpawned()
    {
        // Arrange
        mock.Setup(x => x.PlayerPressesLeftClick()).Returns(true);
        mock.Setup(x => x.IsOnOption()).Returns(true);
        mock.Setup(x => x.GetHitComponent<Option>()).Returns(option);
        option.SetMouseWrapper(mock.Object);
        soulShop.optionPrefab = option;
        soulShop.optionInfos.Add(optionInfo);
        soulShop.OpenShop();
        playerSoulCount.DecreaseSouls();

        // Act
        option.Update();

        // Assert
        var expectedPacks = FindObjectsOfType<Pack>();
        Assert.AreEqual(1, expectedPacks.Length);
    }

    [Test]
    public void UsingStarterPackOption_ClickOnOption_Expect1PackWith2CardsSpawn()
    {
        // Arrange
        option.SetOptionInfo(optionInfo);
        mock.Setup(x => x.PlayerPressesLeftClick()).Returns(true);
        mock.Setup(x => x.IsOnOption()).Returns(true);
        mock.Setup(x => x.GetHitComponent<Option>()).Returns(option);
        option.SetMouseWrapper(mock.Object);
        soulShop.optionPrefab = option;
        soulShop.optionInfos.Add(optionInfo);

        // Act
        option.Update();

        // Assert
        var expectedPack = FindObjectOfType<Pack>();
        Assert.AreEqual(2, expectedPack.GetSize());
    }

    [Test]
    public void UsingRandomMonsterOption_ClickOnOption_Expect1PackWith1CardSpawn()
    {
        // Arrange
        var oneCardPackOptionInfo = TestHelper.GetRandomMonsterOptionInfo();
        option.SetOptionInfo(oneCardPackOptionInfo);
        mock.Setup(x => x.PlayerPressesLeftClick()).Returns(true);
        mock.Setup(x => x.IsOnOption()).Returns(true);
        mock.Setup(x => x.GetHitComponent<Option>()).Returns(option);
        option.SetMouseWrapper(mock.Object);
        soulShop.optionPrefab = option;
        soulShop.optionInfos.Add(optionInfo);

        // Act
        option.Update();

        // Assert
        var expectedPack = FindObjectOfType<Pack>();
        Assert.AreEqual(1, expectedPack.GetSize());
    }

    [Test]
    public void UsingStarterPackOption_ClickOnOption_ExpectNewOptionInfo()
    {
        // Arrange
        var oneCardPackOptionInfo = TestHelper.GetRandomMonsterOptionInfo();
        oneCardPackOptionInfo.cost = 2;
        option.SetOptionInfo(optionInfo);
        mock.Setup(x => x.PlayerPressesLeftClick()).Returns(true);
        mock.Setup(x => x.IsOnOption()).Returns(true);
        mock.Setup(x => x.GetHitComponent<Option>()).Returns(option);
        option.SetMouseWrapper(mock.Object);
        soulShop.optionPrefab = option;
        soulShop.optionInfos.Add(oneCardPackOptionInfo);

        // Act
        option.Update();

        // Assert
        Assert.AreEqual(oneCardPackOptionInfo, option.GetOptionInfo());
    }

    [Test]
    public void UsingOptionCostingMoreThan1Cost_ClickOnOption_ExpectNoPacks()
    {
        // Arrange
        var oneCardPackOptionInfo = TestHelper.GetRandomMonsterOptionInfo();
        oneCardPackOptionInfo.cost = 2;
        option.SetOptionInfo(oneCardPackOptionInfo);
        mock.Setup(x => x.PlayerPressesLeftClick()).Returns(true);
        mock.Setup(x => x.IsOnOption()).Returns(true);
        mock.Setup(x => x.GetHitComponent<Option>()).Returns(option);
        option.SetMouseWrapper(mock.Object);
        soulShop.optionPrefab = option;
        soulShop.optionInfos.Add(oneCardPackOptionInfo);

        // Act
        option.Update();

        // Assert
        var expectedPacks = FindObjectsOfType<Pack>();
        Assert.AreEqual(2, expectedPacks.Length);
    }

    [Test]
    public void UsingOptionCostingMoreThan2Cost_ClickOnOption_ExpectPacksAndNoSouls()
    {
        // Arrange
        var oneCardPackOptionInfo = TestHelper.GetRandomMonsterOptionInfo();
        oneCardPackOptionInfo.cost = 2;
        option.SetOptionInfo(oneCardPackOptionInfo);
        mock.Setup(x => x.PlayerPressesLeftClick()).Returns(true);
        mock.Setup(x => x.IsOnOption()).Returns(true);
        mock.Setup(x => x.GetHitComponent<Option>()).Returns(option);
        option.SetMouseWrapper(mock.Object);
        soulShop.optionPrefab = option;
        soulShop.optionInfos.Add(oneCardPackOptionInfo);
        playerSoulCount.IncreaseSouls();

        // Act
        option.Update();

        // Assert
        var expectedPacks = FindObjectsOfType<Pack>();
        Assert.AreEqual(3, expectedPacks.Length);
        Assert.AreEqual(0, playerSoulCount.GetCurrentSouls());
    }
}
