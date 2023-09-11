using Moq;
using NUnit.Framework;
using UnityEngine;

public class LerpColorTest
{
    [Test]
    public void GivenHalfTime_GetCurrentColor_ExpectHalfColor()
    {
        // Arrange
        var startColor = Color.white;
        var expectColor = Color.red;
        var anyTimePercentage = 0.5f;
        var lerpColor = new LerpColor(startColor, expectColor);

        // Act
        var color = lerpColor.GetCurrentColor(anyTimePercentage);

        // Assert
        Assert.AreEqual(new Color(1.0f, 0.5f, 0.5f, 1.0f), color);
    }

    [Test]
    public void GivenZeroTime_GetCurrentColor_ExpectStart()
    {
        // Arrange
        var startColor = Color.white;
        var expectColor = Color.red;
        var lerpColor = new LerpColor(startColor, expectColor);

        // Act
        var color = lerpColor.GetCurrentColor(0.0f);

        // Assert
        Assert.AreEqual(startColor, color);
    }

    [Test]
    public void GivenOneTime_GetCurrentColor_ExpectEnd()
    {
        // Arrange
        var startColor = Color.white;
        var expectColor = Color.red;
        var lerpColor = new LerpColor(startColor, expectColor);

        // Act
        var color = lerpColor.GetCurrentColor(1.0f);

        // Assert
        Assert.AreEqual(expectColor, color);
    }
}
