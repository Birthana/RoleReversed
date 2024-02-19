using Moq;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ShakeAnimationTest : MonoBehaviour
{
    private readonly static Vector2 ANY_START_POSITION = new Vector2(3, 5);
    private readonly static float ANY_HORIZONTAL_MOVE = 3.0f;
    private readonly static float ANY_VERTICAL_MOVE = 2.0f;
    private readonly static float ANY_ANIMATION_TIME = 0.1f;
    private readonly static Vector2 ANY_MOVEMENT = new Vector2(ANY_HORIZONTAL_MOVE, ANY_VERTICAL_MOVE);
    private GameObject testObject;

    [SetUp]
    public void Setup()
    {
        testObject = new GameObject();
        testObject.transform.position = ANY_START_POSITION;
    }

    [TearDown]
    public void TearDown()
    {
    }

    [Test]
    public void GivenAnimationTime_GetAnimationTime_ExpectAnimationTimeIsEqualToGiven()
    {
        // Arrange
        var shakeAnimation = new ShakeAnimation(testObject.transform, ANY_MOVEMENT, ANY_ANIMATION_TIME);

        // Act
        var animationTime = shakeAnimation.GetAnimationTime();

        // Assert
        Assert.AreEqual(ANY_ANIMATION_TIME, animationTime);
    }

    [UnityTest]
    public IEnumerator GivenStartPosVerticalMoveAndAnimationTime_AnimateFromStartToEnd_ExpectEndPosition()
    {
        // Arrange
        var shakeAnimation = new ShakeAnimation(testObject.transform, ANY_MOVEMENT, ANY_ANIMATION_TIME);

        // Act
        yield return shakeAnimation.AnimateFromStartToEnd();

        // Assert
        Assert.AreEqual(ANY_START_POSITION + ANY_MOVEMENT, shakeAnimation.GetPosition());
    }

    [UnityTest]
    public IEnumerator GivenTransformVerticalMoveAndAnimationTime_AnimateFromEndToStart_ExpectEndPosition()
    {
        // Arrange
        var shakeAnimation = new ShakeAnimation(testObject.transform, ANY_MOVEMENT, ANY_ANIMATION_TIME);

        // Act
        yield return shakeAnimation.AnimateFromEndToStart();

        // Assert
        Assert.AreEqual(ANY_START_POSITION, shakeAnimation.GetPosition());
    }

    [UnityTest]
    public IEnumerator GivenTransformVerticalMoveAndAnimationTime_AnimateFromStartToEnd_ExpectTransform()
    {
        // Arrange
        var shakeAnimation = new ShakeAnimation(testObject.transform, ANY_MOVEMENT, ANY_ANIMATION_TIME);

        // Act
        yield return shakeAnimation.AnimateFromStartToEnd();

        // Assert
        Vector2 actualPosition = testObject.transform.position;
        Assert.AreEqual(ANY_START_POSITION + ANY_MOVEMENT, actualPosition);
    }

    [UnityTest]
    public IEnumerator GivenMovedTransform_AnimateFromStartToEnd_ExpectTransformIsMoved()
    {
        // Arrange
        var shakeAnimation = new ShakeAnimation(testObject.transform, ANY_MOVEMENT, ANY_ANIMATION_TIME);
        var ANY_MOVE_AMOUNT = new Vector2(7, 3);
        testObject.transform.position += (Vector3)ANY_MOVE_AMOUNT;

        // Act
        yield return shakeAnimation.AnimateFromStartToEnd();

        // Assert
        Vector2 actualPosition = testObject.transform.position;
        Assert.AreEqual(ANY_START_POSITION + ANY_MOVE_AMOUNT + ANY_MOVEMENT, actualPosition);
    }

    [UnityTest]
    public IEnumerator GivenMovedTransform_AnimateFromEndToStart_ExpectTransformIsMoved()
    {
        // Arrange
        var shakeAnimation = new ShakeAnimation(testObject.transform, ANY_MOVEMENT, ANY_ANIMATION_TIME);
        var ANY_MOVE_AMOUNT = new Vector2(7, 3);
        testObject.transform.position += (Vector3)ANY_MOVE_AMOUNT;
        yield return shakeAnimation.AnimateFromStartToEnd();

        // Act
        yield return shakeAnimation.AnimateFromEndToStart();

        // Assert
        Vector2 actualPosition = testObject.transform.position;
        Assert.AreEqual(ANY_START_POSITION + ANY_MOVE_AMOUNT, actualPosition);
    }

    [UnityTest]
    public IEnumerator GivenAnimateFromStartToEnd_AnimateFromEndToStart_ExpectTransform()
    {
        // Arrange
        var shakeAnimation = new ShakeAnimation(testObject.transform, ANY_MOVEMENT, ANY_ANIMATION_TIME);
        yield return shakeAnimation.AnimateFromStartToEnd();

        // Act
        yield return shakeAnimation.AnimateFromEndToStart();

        // Assert
        Vector2 actualPosition = testObject.transform.position;
        Assert.AreEqual(ANY_START_POSITION, actualPosition);
    }
}
