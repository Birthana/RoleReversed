using Moq;
using System.Collections;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TMPro;

public class ToolTipTest : MonoBehaviour
{

    [SetUp]
    public void Setup()
    {
    }

    [TearDown]
    public void TearDown()
    {
        FindObjectsOfType<TextMeshPro>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
    }

    [UnityTest]
    public IEnumerator UsingToolTipManager_GetText_ExpectCardDescription()
    {
        // Arrange
        var toolTip = new GameObject().AddComponent<ToolTipManager>();
        var parent = new GameObject();
        var text = new GameObject().AddComponent<TextMeshPro>();
        text.transform.SetParent(parent.transform);
        toolTip.toolTipPrefab = parent;

        // Act
        toolTip.SetText("ANY_CARD_DESCRIPTION");
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
        var toolTip = new GameObject().AddComponent<ToolTipManager>();
        var parent = new GameObject();
        var text = new GameObject().AddComponent<TextMeshPro>();
        text.transform.SetParent(parent.transform);
        toolTip.toolTipPrefab = parent;
        var position = new Vector3(3, 5, 0);

        // Act
        toolTip.SetText("ANY_CARD_DESCRIPTION", position);
        yield return null;

        // Assert
        var expectedToolTip = FindObjectsOfType<TextMeshPro>();
        Assert.AreEqual(position, expectedToolTip[0].transform.parent.position);
    }

    [UnityTest]
    public IEnumerator UsingToolTipManager_Clear_ExpectPosition()
    {
        // Arrange
        var toolTip = new GameObject().AddComponent<ToolTipManager>();
        var parent = new GameObject();
        var text = new GameObject().AddComponent<TextMeshPro>();
        text.transform.SetParent(parent.transform);
        toolTip.toolTipPrefab = parent;
        toolTip.SetText("ANY_CARD_DESCRIPTION");

        // Act
        toolTip.Clear();
        yield return null;

        // Assert
        var expectedToolTip = FindObjectsOfType<TextMeshPro>();
        Assert.AreEqual(1, expectedToolTip.Length);
    }

    [UnityTest]
    public IEnumerator UsingToolTipManagerWithClear_Set_ExpectPosition()
    {
        // Arrange
        var toolTip = new GameObject().AddComponent<ToolTipManager>();
        var parent = new GameObject();
        var text = new GameObject().AddComponent<TextMeshPro>();
        text.transform.SetParent(parent.transform);
        toolTip.toolTipPrefab = parent;
        toolTip.SetText("ANY_CARD_DESCRIPTION");
        toolTip.Clear();

        // Act
        toolTip.SetText("ANY_CARD_DESCRIPTION");
        yield return null;

        // Assert
        var expectedToolTip = FindObjectsOfType<TextMeshPro>();
        Assert.AreEqual(true, expectedToolTip[0].transform.parent.gameObject.activeInHierarchy);
    }
}
