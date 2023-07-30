using NUnit.Framework;
using UnityEngine;

public class CenterPositionTest
{
    private readonly static Vector3 ANY_VECTOR_3 = new Vector3(3, 5, 9);
    private readonly static int ANY_SIZE = 5;
    private readonly static int ANY_INDEX = 3;
    private readonly static float ANY_SPACING = 7.0f;
    private readonly CenterPosition centerPosition_ = new CenterPosition(ANY_VECTOR_3, ANY_SIZE, ANY_SPACING);

    public float GetOffset()
    {
        float positionOffset = ANY_INDEX - ((float)ANY_SIZE - 1) / 2;
        return Mathf.Sin(positionOffset * Mathf.Deg2Rad) * ANY_SPACING * 10;
    }

    [Test]
    public void GetCenterPosition_ExpectCenterPositionIsVectorGiven()
    {
        // Arrange

        // Act
        var position = centerPosition_.GetCenterPosition();

        // Assert
        Assert.AreEqual(ANY_VECTOR_3, position);
    }

    [Test]
    public void GetSize_ExpectSizeIsSizeGiven()
    {
        // Arrange

        // Act
        var size = centerPosition_.GetSize();

        // Assert
        Assert.AreEqual(ANY_SIZE, size);
    }

    [Test]
    public void GivenAnyIndex_GetHorizontalOffsetPositionAt_ExpectPositionIsCorrect()
    {
        // Arrange

        // Act
        var position = centerPosition_.GetHorizontalOffsetPositionAt(ANY_INDEX);

        // Assert
        Assert.AreEqual(ANY_VECTOR_3.x + GetOffset(), position.x);
        Assert.AreEqual(ANY_VECTOR_3.y, position.y);
        Assert.AreEqual(ANY_VECTOR_3.z, position.z);
    }

    [Test]
    public void GivenAnyIndex_GetVerticalOffsetPositionAt_ExpectPositionIsCorrect()
    {
        // Arrange

        // Act
        var position = centerPosition_.GetVerticalOffsetPositionAt(ANY_INDEX);

        // Assert
        Assert.AreEqual(ANY_VECTOR_3.x, position.x);
        Assert.AreEqual(ANY_VECTOR_3.y + GetOffset(), position.y);
        Assert.AreEqual(ANY_VECTOR_3.z, position.z);
    }
}
