using Moq;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SpaceManagerTest : MonoBehaviour
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
    public void UsingSpaceManager_ExpectSpaceIsZero()
    {
        // Arrange
        var spaceManager = new GameObject().AddComponent<SpaceManager>();

        // Act

        // Assert
        Assert.AreEqual(0, spaceManager.GetCount());
    }

    [Test]
    public void UsingSpaceManager_SpawnSpace_ExpectSpaceIsZero()
    {
        // Arrange
        var spaceManager = new GameObject().AddComponent<SpaceManager>();

        // Act

        // Assert
        Assert.AreEqual(0, spaceManager.GetCount());
    }

    [Test]
    public void UsingSpaceManagerWithSetupSpacePositions_SpawnSpace_ExpectSpaceIs14()
    {
        // Arrange
        var spaceManager = new GameObject().AddComponent<SpaceManager>();
        spaceManager.numberOfSpaces = new Vector2(5, 3);
        spaceManager.SetupSpaces();

        // Act

        // Assert
        Assert.AreEqual(14, spaceManager.GetCount());
    }

    [Test]
    public void UsingSpaceManagerWithSetupSpacePositions_SpawnSpace_ExpectSpaceIs11()
    {
        // Arrange
        var spaceManager = new GameObject().AddComponent<SpaceManager>();
        spaceManager.numberOfSpaces = new Vector2(5, 3);
        spaceManager.SetupSpaces();

        // Act

        // Assert
        Assert.AreEqual(11, spaceManager.GetCount());
    }
}
