using Moq;
using NUnit.Framework;
using UnityEngine;

public class PlayerSoulCounterTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void UsingPlayerSoulCounter_GetCurrentSouls_ExpectZero()
    {
        // Arrange
        var counter = new GameObject().AddComponent<PlayerSoulCounter>();

        // Act
        var souls = counter.GetCurrentSouls();

        // Assert
        Assert.AreEqual(0, souls);
    }

    [Test]
    public void UsingPlayerSoulCounter_IncreaseSouls_ExpectOne()
    {
        // Arrange
        var counter = new GameObject().AddComponent<PlayerSoulCounter>();

        // Act
        counter.IncreaseSouls();

        // Assert
        Assert.AreEqual(1, counter.GetCurrentSouls());
    }

    [Test]
    public void UsingPlayerSoulCounterWithOneSoul_DecreaseSouls_ExpectZero()
    {
        // Arrange
        var counter = new GameObject().AddComponent<PlayerSoulCounter>();
        counter.IncreaseSouls();

        // Act
        counter.DecreaseSouls();

        // Assert
        Assert.AreEqual(0, counter.GetCurrentSouls());
    }
}
