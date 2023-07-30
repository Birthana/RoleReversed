using Moq;
using NUnit.Framework;
using UnityEngine;

public class LerpPositionTest
{
    private readonly static Vector2 ANY_START_POSITION = new Vector2(3, 5);
    private readonly static float ANY_VERTICAL_MOVE = 2.0f;

    [Test]
    public void GivenStartPosition_GetStartPosition_ExpectStartPositionIsEqualToGiven()
    {
        // Arrange
        LerpPosition hoverAnimation = new LerpPosition(ANY_START_POSITION);

        // Act
        var startPosition = hoverAnimation.GetStartPosition();

        // Assert
        Assert.AreEqual(ANY_START_POSITION, startPosition);
    }

    [Test]
    public void GivenVerticalMove_GetVerticalMove_ExpectVerticalMoveIsEqualToGiven()
    {
        // Arrange
        LerpPosition hoverAnimation = new LerpPosition(ANY_START_POSITION, ANY_VERTICAL_MOVE);

        // Act
        var verticalMove = hoverAnimation.GetVerticalMove();

        // Assert
        Assert.AreEqual(ANY_VERTICAL_MOVE, verticalMove);
    }

    [Test]
    public void GivenStartPositionAndVerticalMove_GetCurrentPosition_ExpectCurrentPosition()
    {
        // Arrange
        var ANY_TIME_PERCENTAGE = 0.5f;
        LerpPosition hoverAnimation = new LerpPosition(ANY_START_POSITION, ANY_VERTICAL_MOVE);

        // Act
        var currentPosition = hoverAnimation.GetCurrentPosition(ANY_TIME_PERCENTAGE);

        // Assert
        var resultPosition = ANY_START_POSITION + new Vector2(0, ANY_VERTICAL_MOVE * ANY_TIME_PERCENTAGE);
        Assert.AreEqual(resultPosition, currentPosition);
    }
}
