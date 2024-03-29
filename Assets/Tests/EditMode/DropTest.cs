using Moq;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class DropTest : MonoBehaviour
{
    private DropCount dropCount;

    [SetUp]
    public void Setup()
    {
        dropCount = new GameObject().AddComponent<DropCount>();
    }

    [TearDown]
    public void TearDown()
    {
    }

    [Test]
    public void UsingDrop_Add_ExpectSizeIncrease()
    {
        // Arrange
        var cardInfo = TestHelper.GetAnyMonsterCardInfo();
        var drop = TestHelper.GetDrop();

        // Act
        drop.Add(cardInfo);

        // Assert
        Assert.AreEqual(1, drop.GetSize());
    }

    [Test]
    public void UsingDropWith1Card_ReturnCardsToDeck_ExpectSizeZero()
    {
        // Arrange
        var cardInfo = TestHelper.GetAnyMonsterCardInfo();
        var drop = TestHelper.GetDrop();
        drop.Add(cardInfo);

        // Act
        drop.ReturnCardsToDeck();

        // Assert
        Assert.AreEqual(0, drop.GetSize());
    }

    //[Test]
    //public void UsingDrop_Add_ExpectCardSprite()
    //{
    //    // Arrange
    //    var cardInfo = TestHelper.GetAnyMonsterCardInfo();
    //    var drop = TestHelper.GetDrop();

    //    // Act
    //    drop.Add(cardInfo);

    //    // Assert
    //    Assert.AreEqual(cardInfo.fieldSprite, drop.GetComponent<SpriteRenderer>().sprite);
    //}

    [Test]
    public void UsingDropWith1Card_ReturnCardsToDeck_ExpectNoCardSprite()
    {
        // Arrange
        var cardInfo = TestHelper.GetAnyMonsterCardInfo();
        var drop = TestHelper.GetDrop();
        drop.Add(cardInfo);

        // Act
        drop.ReturnCardsToDeck();

        // Assert
        Assert.AreEqual(null, drop.GetComponent<SpriteRenderer>().sprite);
    }
}
