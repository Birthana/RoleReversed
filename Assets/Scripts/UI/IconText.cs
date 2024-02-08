using UnityEngine;
using TMPro;

public class IconText
{
    private Color color = Color.white;
    private static readonly string NUMBERS_SPRITE_ASSET_FILE_PATH = "SpriteAssets/Numbers";

    public IconText(Color color)
    {
        this.color = color;
    }

    public string GetNumbersText(string text)
    {
        var result = text;
        var numbers = Resources.Load<TMP_SpriteAsset>(NUMBERS_SPRITE_ASSET_FILE_PATH);

        foreach (var number in numbers.spriteCharacterTable)
        {
            if (result.Contains(number.name))
            {
                result = result.Replace(number.name, $"<sprite name=\"{number.name}\" PLACEHOLDER>");
            }
        }

        return result.Replace("PLACEHOLDER", $"color=#{ColorUtility.ToHtmlStringRGBA(color)}"); ;
    }
}
