using Moq;
using NUnit.Framework;
using UnityEngine;

public class LerpPositionTest
{
    private readonly static Vector2 ANY_START_POSITION = new Vector2(3, 5);
    private readonly static float ANY_HORIZONTAL_MOVE = 3.0f;
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
        var movement = new Vector2(0, ANY_VERTICAL_MOVE);
        LerpPosition hoverAnimation = new LerpPosition(ANY_START_POSITION, movement);

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
        var movement = new Vector2(0, ANY_VERTICAL_MOVE);
        LerpPosition hoverAnimation = new LerpPosition(ANY_START_POSITION, movement);

        // Act
        var currentPosition = hoverAnimation.GetCurrentPosition(ANY_TIME_PERCENTAGE);

        // Assert
        var resultPosition = ANY_START_POSITION + new Vector2(0, ANY_VERTICAL_MOVE * ANY_TIME_PERCENTAGE);
        Assert.AreEqual(resultPosition, currentPosition);
    }

    [Test]
    public void GivenHorizontalMove_GetHorizontalMove_ExpectHorizontalMoveIsEqualToGiven()
    {
        // Arrange
        var movement = new Vector2(ANY_HORIZONTAL_MOVE, 0);
        LerpPosition hoverAnimation = new LerpPosition(ANY_START_POSITION, movement);

        // Act
        var horizontal = hoverAnimation.GetHorizontalMove();

        // Assert
        Assert.AreEqual(ANY_HORIZONTAL_MOVE, horizontal);
    }
}
