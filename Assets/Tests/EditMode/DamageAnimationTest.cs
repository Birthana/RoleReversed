using Moq;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class DamageAnimationTest
{
    private readonly static Color ANY_END_COLOR = Color.red;
    private readonly static float ANY_TIME = 1.0f;

    [UnityTest]
    public IEnumerator GivenSpriteRender_AnimateFromStartToEnd_ExpectEndColor()
    {
        // Arrange
        var gameObject = new GameObject();
        gameObject.AddComponent<SpriteRenderer>();
        var spriteRender = gameObject.GetComponent<SpriteRenderer>();
        var damageAnimation = new DamageAnimation(spriteRender, ANY_END_COLOR, ANY_TIME);

        // Act
        yield return damageAnimation.AnimateFromStartToEnd();

        // Assert
        Assert.AreEqual(ANY_END_COLOR, damageAnimation.GetColor());
    }

    [UnityTest]
    public IEnumerator GivenSpriteRender_AnimateFromEndToStart_ExpectStartColor()
    {
        // Arrange
        var gameObject = new GameObject();
        gameObject.AddComponent<SpriteRenderer>();
        var spriteRender = gameObject.GetComponent<SpriteRenderer>();
        var startColor = spriteRender.color;
        var damageAnimation = new DamageAnimation(spriteRender, ANY_END_COLOR, ANY_TIME);

        // Act
        yield return damageAnimation.AnimateFromEndToStart();

        // Assert
        Assert.AreEqual(startColor, damageAnimation.GetColor());
    }

    [UnityTest]
    public IEnumerator GivenAnimateFromStartToEnd_AnimateFromEndToStart_ExpectEndColor()
    {
        // Arrange
        var gameObject = new GameObject();
        gameObject.AddComponent<SpriteRenderer>();
        var spriteRender = gameObject.GetComponent<SpriteRenderer>();
        var startColor = spriteRender.color;
        var damageAnimation = new DamageAnimation(spriteRender, ANY_END_COLOR, ANY_TIME);
        yield return damageAnimation.AnimateFromStartToEnd();

        // Act
        yield return damageAnimation.AnimateFromEndToStart();

        // Assert
        Assert.AreEqual(startColor, damageAnimation.GetColor());
    }
}
