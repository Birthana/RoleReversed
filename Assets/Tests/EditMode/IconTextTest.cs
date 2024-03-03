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

    [Test]
    public void UsingIconTextWithResultText_GetNumbersText_ExpectSpriteAssetText()
    {
        // Arrange
        var icons = new IconText(Color.white);
        string numberText = "1";
        icons.GetNumbersText(numberText);
        numberText = "2";

        // Act
        var result = icons.GetNumbersText(numberText);

        // Assert
        Assert.AreEqual($"<sprite name=\"{numberText}\" color=#{ColorUtility.ToHtmlStringRGBA(Color.white)}>", result);
    }


    [Test]
    public void UsingIconTextWithResultText_GetNumbersTextWith13_ExpectSpriteAssetText()
    {
        // Arrange
        var icons = new IconText(Color.white);
        string numberText = "1";
        icons.GetNumbersText(numberText);
        numberText = "13";

        // Act
        var result = icons.GetNumbersText(numberText);

        // Assert
        Assert.AreEqual($"<sprite name=\"1\" color=#{ColorUtility.ToHtmlStringRGBA(Color.white)}>" +
                        $"<sprite name=\"3\" color=#{ColorUtility.ToHtmlStringRGBA(Color.white)}>", result);
    }
}
