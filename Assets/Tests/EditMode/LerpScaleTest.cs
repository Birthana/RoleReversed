using Moq;
using NUnit.Framework;
using UnityEngine;

public class LerpScaleTest
{
    [Test]
    public void GivenHalfTime_GetCurrentScale_ExpectHalfScale()
    {
        // Arrange
        var startScale = 1;
        var endScale = 3;
        var anyTimePercentage = 0.5f;
        var lerpScale = new LerpScale(startScale, endScale);

        // Act
        var scale = lerpScale.GetCurrentScale(anyTimePercentage);

        // Assert
        Assert.AreEqual(2, scale);
    }

    [Test]
    public void GivenZeroTime_GetCurrentScale_ExpectStartScale()
    {
        // Arrange
        var startScale = 1;
        var endScale = 3;
        var lerpScale = new LerpScale(startScale, endScale);

        // Act
        var scale = lerpScale.GetCurrentScale(0.0f);

        // Assert
        Assert.AreEqual(startScale, scale);
    }

    [Test]
    public void GivenFullTime_GetCurrentScale_ExpectEndScale()
    {
        // Arrange
        var startScale = 1;
        var endScale = 3;
        var lerpScale = new LerpScale(startScale, endScale);

        // Act
        var scale = lerpScale.GetCurrentScale(1.0f);

        // Assert
        Assert.AreEqual(endScale, scale);
    }
}
