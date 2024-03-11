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
        toolTip.gameObject.AddComponent<TextMeshPro>();
        toolTip.SetToolTipText("ANY_CARD_DESCRIPTION");

        // Act
        var text = toolTip.GetText();
        yield return null;

        // Assert
        Assert.AreEqual("ANY_CARD_DESCRIPTION", text);
        var textMeshPro = FindObjectsOfType<TextMeshPro>();
        Assert.AreEqual(1, textMeshPro.Length);
    }

    [UnityTest]
    public IEnumerator UsingToolTipManager_GetText_ExpectEmpty()
    {
        // Arrange
        var toolTip = new GameObject().AddComponent<ToolTipManager>();
        toolTip.gameObject.AddComponent<TextMeshPro>();
        toolTip.SetToolTipText("ANY_CARD_DESCRIPTION");

        // Act
        toolTip.Clear();
        yield return null;

        // Assert
        var textMeshPro = FindObjectOfType<TextMeshPro>();
        Assert.AreEqual("", textMeshPro.text);
    }

    [UnityTest]
    public IEnumerator UsingToolTipManager_SetText_ExpectPosition()
    {
        // Arrange
        var toolTip = new GameObject().AddComponent<ToolTipManager>();
        toolTip.gameObject.AddComponent<TextMeshPro>();

        // Act
        toolTip.SetToolTipText("ANY_CARD_DESCRIPTION", new Vector2(3, 5));
        yield return null;

        // Assert
        Assert.AreEqual(new Vector3(3, 5, 0), toolTip.gameObject.transform.position);
    }
}
