using Moq;
using System.Collections;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ScaleAnimationTest : MonoBehaviour
{
    private static float ANY_SCALE = 3;

    [SetUp]
    public void Setup()
    {
    }

    [TearDown]
    public void TearDown()
    {
    }

    [UnityTest]
    public IEnumerator UsingScaleAnimation_GetCurrentScale_ExpectScaleIsDefault()
    {
        // Arrange
        var gameObject = new GameObject();
        var scaleAnimation = new ScaleAnimation(gameObject.transform, ANY_SCALE, 1.0f);

        // Act
        yield return null;

        // Assert
        Assert.AreEqual(1, scaleAnimation.GetCurrentScale());
    }

    [UnityTest]
    public IEnumerator UsingScaleAnimation_AnimateStartToEnd_ExpectScaleIsEnd()
    {
        // Arrange
        var gameObject = new GameObject();
        var scaleAnimation = new ScaleAnimation(gameObject.transform, ANY_SCALE, 1.0f);

        // Act
        yield return scaleAnimation.AnimateStartToEnd();

        // Assert
        Assert.AreEqual(3, scaleAnimation.GetCurrentScale());
    }

    [UnityTest]
    public IEnumerator UsingScaleAnimation_AnimateEndToStart_ExpectScaleIsStart()
    {
        // Arrange
        var gameObject = new GameObject();
        var scaleAnimation = new ScaleAnimation(gameObject.transform, ANY_SCALE, 1.0f);

        // Act
        yield return scaleAnimation.AnimateEndToStart();

        // Assert
        Assert.AreEqual(1, scaleAnimation.GetCurrentScale());
    }

    [UnityTest]
    public IEnumerator UsingScaleAnimationAnimateStartToEnd_AnimateEndToStart_ExpectScaleIsStart()
    {
        // Arrange
        var gameObject = new GameObject();
        var scaleAnimation = new ScaleAnimation(gameObject.transform, ANY_SCALE, 1.0f);
        yield return scaleAnimation.AnimateStartToEnd();

        // Act
        yield return scaleAnimation.AnimateEndToStart();

        // Assert
        Assert.AreEqual(1, scaleAnimation.GetCurrentScale());
    }
}
