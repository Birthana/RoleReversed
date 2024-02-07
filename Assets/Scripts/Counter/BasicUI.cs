using UnityEngine;
using TMPro;

public class BasicUI : MonoBehaviour
{
    public TextMeshPro ui;
    public Color color = Color.white;
    private static readonly string NUMBERS_SPRITE_ASSET_FILE_PATH = "SpriteAssets/Numbers";

    public virtual void Display(int health)
    {
        ui.text = $"{GetNumbersText("" + health)}";
    }

    private string GetNumbersText(string text)
    {
        var result = text;
        var numbers = Resources.Load<TMP_SpriteAsset>(NUMBERS_SPRITE_ASSET_FILE_PATH);
        
        foreach (var number in numbers.spriteCharacterTable)
        {
            if (result.Contains(number.name))
            {
                result = result.Replace(number.name, $"<sprite name=\"{number.name}\">");
            }
        }

        foreach (var number in numbers.spriteCharacterTable)
        {
            if (result.Contains(number.name))
            {
                result = result.Replace($"<sprite name=\"{number.name}\">", 
                                        $"<sprite name=\"{number.name}\" color=#{ColorUtility.ToHtmlStringRGBA(color)}>");
            }
        }

        return result;
    }
}
