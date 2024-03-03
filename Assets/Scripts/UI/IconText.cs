using UnityEngine;
using TMPro;

public class IconText
{
    private Color color = Color.white;

    public IconText(Color color)
    {
        this.color = color;
    }

    public string GetNumbersText(string text)
    {
        return GetNumbersTextThruReplacement(text);
    }

    private string GetNumbersTextThruReplacement(string text)
    {
        var result = "";
        var charArray = text.ToCharArray();
        for(int i = 0; i < charArray.Length; i++)
        {
            var replace = "" + charArray[i];
            result += $"<sprite name=\"{replace}\" color=#{ColorUtility.ToHtmlStringRGBA(color)}>";
        }

        return result;
    }
}
