using Moq;
using NUnit.Framework;
using System.Linq;
using UnityEngine;

public class ChangeSortingLayerTest : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer spriteRendererChild;

    [SetUp]
    public void Setup()
    {
        spriteRenderer = new GameObject().AddComponent<SpriteRenderer>();
        spriteRenderer.sortingLayerName = "Default";
        spriteRendererChild = new GameObject().AddComponent<SpriteRenderer>();
        spriteRendererChild.sortingLayerName = "Default";
        spriteRendererChild.transform.SetParent(spriteRenderer.transform);
    }

    [TearDown]
    public void TearDown()
    {
    }

    [Test]
    public void UsingChangeSortingLayer_SetToCurrentRoom_ExpectSortingLayerIsCurrentRoom()
    {
        // Arrange
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
        var changeLayer = new ChangeSortingLayer(spriteRenderer.gameObject);

        // Act
        changeLayer.SetToCurrentRoom();

        // Assert
        Assert.AreEqual("CurrentRoom", spriteRendererChild.sortingLayerName);
    }

    [Test]
    public void UsingChangeSortingLayerWithChildren_SetToDefault_ExpectSortingLayerIsDefault()
    {
        // Arrange
        var changeLayer = new ChangeSortingLayer(spriteRenderer.gameObject);
        changeLayer.SetToCurrentRoom();

        // Act
        changeLayer.SetToDefault();

        // Assert
        Assert.AreEqual("Default", spriteRendererChild.sortingLayerName);
    }

}
