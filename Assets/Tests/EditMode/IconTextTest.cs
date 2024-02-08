using Moq;
using NUnit.Framework;
using UnityEngine;

public class IconTextTest
{
    [Test]
    public void UsingIconText_GetNumbersText_ExpectSpriteAssetText()
    {
        // Arrange
        var icons = new IconText(Color.white);
        string numberText = "1";

        // Act
        var result = icons.GetNumbersText(numberText);

        // Assert
        Assert.AreEqual($"<sprite name=\"{numberText}\" color=#{ColorUtility.ToHtmlStringRGBA(Color.white)}>", result);
    }
}
