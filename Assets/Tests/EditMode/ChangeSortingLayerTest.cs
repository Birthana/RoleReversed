using Moq;
using NUnit.Framework;
using System.Linq;
using UnityEngine;

public class ChangeSortingLayerTest : MonoBehaviour
{
    [SetUp]
    public void Setup()
    {
    }

    [TearDown]
    public void TearDown()
    {
    }

    [Test]
    public void UsingChangeSortingLayer_SetToCurrentRoom_ExpectSortingLayerIsCurrentRoom()
    {
        // Arrange
        var spriteRenderer = new GameObject().AddComponent<SpriteRenderer>();
        spriteRenderer.sortingLayerName = "Default";
        var changeLayer = new ChangeSortingLayer(spriteRenderer.gameObject);

        // Act
        changeLayer.SetToCurrentRoom();

        // Assert
        Assert.AreEqual("CurrentRoom", spriteRenderer.sortingLayerName);
    }

    [Test]
    public void UsingChangeSortingLayerWithChildren_SetToCurrentRoom_ExpectSortingLayerIsCurrentRoom()
    {
        // Arrange
        var spriteRendererParent = new GameObject().AddComponent<SpriteRenderer>();
        spriteRendererParent.sortingLayerName = "Default";
        var changeLayer = new ChangeSortingLayer(spriteRendererParent.gameObject);
        var spriteRendererChild = new GameObject().AddComponent<SpriteRenderer>();
        spriteRendererChild.sortingLayerName = "Default";
        spriteRendererChild.transform.SetParent(spriteRendererParent.transform);

        // Act
        changeLayer.SetToCurrentRoom();

        // Assert
        Assert.AreEqual("CurrentRoom", spriteRendererChild.sortingLayerName);
    }

    [Test]
    public void UsingChangeSortingLayerWithChildren_SetToDefault_ExpectSortingLayerIsDefault()
    {
        // Arrange
        var spriteRendererParent = new GameObject().AddComponent<SpriteRenderer>();
        spriteRendererParent.sortingLayerName = "CurrentRoom";
        var changeLayer = new ChangeSortingLayer(spriteRendererParent.gameObject);
        var spriteRendererChild = new GameObject().AddComponent<SpriteRenderer>();
        spriteRendererChild.sortingLayerName = "CurrentRoom";
        spriteRendererChild.transform.SetParent(spriteRendererParent.transform);

        // Act
        changeLayer.SetToDefault();

        // Assert
        Assert.AreEqual("Default", spriteRendererChild.sortingLayerName);
    }

}
