using UnityEngine;

public class DescriptionText
{
    public string GetText(string text)
    {
        var iconText = new IconText(Color.white);
        return iconText.GetNumbersText(text);
    }
}
