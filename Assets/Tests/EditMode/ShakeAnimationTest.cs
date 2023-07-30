using Moq;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ShakeAnimationTest : MonoBehaviour
{
    private readonly static Vector2 ANY_START_POSITION = new Vector2(3, 5);
    private readonly static float ANY_VERTICAL_MOVE = 2.0f;
    private readonly static float ANY_ANIMATION_TIME = 0.1f;

    [Test]
    public void GivenAnimationTime_GetAnimationTime_ExpectAnimationTimeIsEqualToGiven()
    {
        // Arrange
        var shakeAnimation = new ShakeAnimation(ANY_ANIMATION_TIME);

        // Act
        var animationTime = shakeAnimation.GetAnimationTime();

        // Assert
        Assert.AreEqual(ANY_ANIMATION_TIME, animationTime);
    }

    [UnityTest]
    public IEnumerator GivenStartPosVerticalMoveAndAnimationTime_AnimateFromStartToEnd_ExpectEndPosition()
    {
        // Arrange
        var shakeAnimation = new ShakeAnimation(ANY_START_POSITION, ANY_VERTICAL_MOVE, ANY_ANIMATION_TIME);

        // Act
        yield return shakeAnimation.AnimateFromStartToEnd();

        // Assert
        Assert.AreEqual(ANY_START_POSITION + new Vector2(0, ANY_VERTICAL_MOVE), shakeAnimation.GetPosition());
    }

    [UnityTest]
    public IEnumerator GivenTransformVerticalMoveAndAnimationTime_AnimateFromEndToStart_ExpectEndPosition()
    {
        // Arrange
        var gameObject = new GameObject();
        gameObject.transform.position = ANY_START_POSITION;
        var shakeAnimation = new ShakeAnimation(gameObject.transform, ANY_VERTICAL_MOVE, ANY_ANIMATION_TIME);

        // Act
        yield return shakeAnimation.AnimateFromEndToStart();

        // Assert
        Assert.AreEqual(ANY_START_POSITION, shakeAnimation.GetPosition());
    }

    [UnityTest]
    public IEnumerator GivenTransformVerticalMoveAndAnimationTime_AnimateFromEndToStart_ExpectTransform()
    {
        // Arrange
        var gameObject = new GameObject();
        gameObject.transform.position = ANY_START_POSITION;
        var shakeAnimation = new ShakeAnimation(gameObject.transform, ANY_VERTICAL_MOVE, ANY_ANIMATION_TIME);

        // Act
        yield return shakeAnimation.AnimateFromStartToEnd();

        // Assert
        Vector2 actualPosition = gameObject.transform.position;
        Assert.AreEqual(ANY_START_POSITION + new Vector2(0, ANY_VERTICAL_MOVE), actualPosition);
    }
}
