using Moq;
using System.Linq;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class DamageAnimationTest : MonoBehaviour
{
    private readonly static Color ANY_END_COLOR = Color.red;
    private readonly static float ANY_TIME = 1.0f;
    private SpriteRenderer spriteRenderer;

    [SetUp]
    public void Setup()
    {
        spriteRenderer = new GameObject().AddComponent<SpriteRenderer>();
    }

    [TearDown]
    public void TearDown()
    {
    }

    [UnityTest]
    public IEnumerator GivenSpriteRender_AnimateFromStartToEnd_ExpectEndColor()
    {
        // Arrange;
        var damageAnimation = new DamageAnimation(spriteRenderer, ANY_END_COLOR, ANY_TIME);

        // Act
        yield return damageAnimation.AnimateFromStartToEnd();

        // Assert
        Assert.AreEqual(ANY_END_COLOR, damageAnimation.GetColor());
    }

    [UnityTest]
    public IEnumerator GivenSpriteRender_AnimateFromEndToStart_ExpectStartColor()
    {
        // Arrange
        var damageAnimation = new DamageAnimation(spriteRenderer, ANY_END_COLOR, ANY_TIME);

        // Act
        yield return damageAnimation.AnimateFromEndToStart();

        // Assert
        var startColor = spriteRenderer.color;
        Assert.AreEqual(startColor, damageAnimation.GetColor());
    }

    [UnityTest]
    public IEnumerator GivenAnimateFromStartToEnd_AnimateFromEndToStart_ExpectEndColor()
    {
        // Arrange
        var damageAnimation = new DamageAnimation(spriteRenderer, ANY_END_COLOR, ANY_TIME);
        yield return damageAnimation.AnimateFromStartToEnd();

        // Act
        yield return damageAnimation.AnimateFromEndToStart();

        // Assert
        var startColor = spriteRenderer.color;
        Assert.AreEqual(startColor, damageAnimation.GetColor());
    }
}
