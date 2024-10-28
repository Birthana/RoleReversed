using Moq;
using System.Collections;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TMPro;

public class ToolTipTest : MonoBehaviour
{
    private ToolTipManager toolTip;

    [SetUp]
    public void Setup()
    {
        toolTip = TestHelper.GetToolTipManager();
    }

    [TearDown]
    public void TearDown()
    {
        FindObjectsOfType<TextMeshPro>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
        FindObjectsOfType<ToolTipManager>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
    }

    [UnityTest]
    public IEnumerator UsingToolTipManager_GetText_ExpectCardDescription()
    {
        // Arrange
        var cardInfo = TestHelper.GetAnyMonsterCardInfo();
        cardInfo.SetDescription("ANY_CARD_DESCRIPTION");

        // Act
        toolTip.SetText(cardInfo);
        yield return null;

        // Assert
        var expectedToolTip = FindObjectsOfType<TextMeshPro>();
        Assert.AreEqual(1 + 1, expectedToolTip.Length);
        Assert.AreEqual("ANY_CARD_DESCRIPTION", expectedToolTip[0].text);
    }

    [UnityTest]
    public IEnumerator UsingToolTipManagerWithPosition_GetText_ExpectPosition()
    {
        // Arrange
        var position = new Vector3(3, 5, 0);
        var cardInfo = TestHelper.GetAnyMonsterCardInfo();
        cardInfo.SetDescription("ANY_CARD_DESCRIPTION");
        
        // Act
        toolTip.Set(cardInfo, position);
        yield return null;

        // Assert
        var expectedToolTip = FindObjectsOfType<TextMeshPro>();
        Assert.AreEqual(position, expectedToolTip[0].transform.parent.position);
    }

    [UnityTest]
    public IEnumerator UsingToolTipManager_Clear_ExpectPosition()
    {
        // Arrange
        var cardInfo = TestHelper.GetAnyMonsterCardInfo();
        cardInfo.SetDescription("ANY_CARD_DESCRIPTION");
        toolTip.SetText(cardInfo);

        // Act
        toolTip.Clear();
        yield return null;

        // Assert
        var expectedToolTip = FindObjectsOfType<TextMeshPro>();
        Assert.AreEqual(1 + 1, expectedToolTip.Length);
    }

    [UnityTest]
    public IEnumerator UsingToolTipManagerWithClear_Set_ExpectPosition()
    {
        // Arrange
        var cardInfo = TestHelper.GetAnyMonsterCardInfo();
        cardInfo.SetDescription("ANY_CARD_DESCRIPTION");
        toolTip.SetText(cardInfo);
        toolTip.Clear();

        // Act
        toolTip.SetText(cardInfo);
        yield return null;

        // Assert
        var expectedToolTip = FindObjectsOfType<TextMeshPro>();
        Assert.AreEqual(true, expectedToolTip[0].transform.parent.gameObject.activeInHierarchy);
    }

    [UnityTest]
    public IEnumerator UsingToolTipManager_SetEmpty_ExpectNoToolTip()
    {
        // Arrange
        var cardInfo = TestHelper.GetAnyMonsterCardInfo();
        cardInfo.SetDescription("");

        // Act
        toolTip.SetText(cardInfo);
        yield return null;

        // Assert
        var expectedToolTip = FindObjectsOfType<TextMeshPro>();
        Assert.AreEqual(1, expectedToolTip.Length);
    }

    [UnityTest]
    public IEnumerator UsingToolTipManager_SetEmptyPosition_ExpectNoToolTip()
    {
        // Arrange
        var cardInfo = TestHelper.GetAnyMonsterCardInfo();
        cardInfo.SetDescription("");

        // Act
        toolTip.Set(cardInfo, new Vector3(3, 5));
        yield return null;

        // Assert
        var expectedToolTip = FindObjectsOfType<TextMeshPro>();
        Assert.AreEqual(1, expectedToolTip.Length);
    }

    [UnityTest]
    public IEnumerator XXX()
    {
        // Arrange
        var cardInfo = TestHelper.GetAnyMonsterCardInfo();
        cardInfo.SetDescription("");

        // Act
        toolTip.Set(cardInfo, new Vector3(3, 5));
        yield return null;

        // Assert
        var expectedToolTip = FindObjectsOfType<TextMeshPro>();
        Assert.AreEqual(1, expectedToolTip.Length);
    }
}
