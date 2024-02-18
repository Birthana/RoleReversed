using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EffectText
{
    private static readonly List<string> keywords =
        new List<string>() { "Room", "Monster", "Slime" , "Action", "Draw", "Random", "Capacity", "Damage"
            , "Battle Start", "Entrance", "Exit", "Engage" };
    private static readonly string EFFECT_TEXT_ASSET_FILE_PATH = "SpriteAssets/Effect_Text";
    private TMP_StyleSheet styleSheet;


    public string GetText(string text)
    {
        var result = text;
        styleSheet = Resources.Load<TMP_StyleSheet>(EFFECT_TEXT_ASSET_FILE_PATH);

        foreach (var keyword in keywords)
        {
            if (styleSheet.GetStyle(keyword) == null)
            {
                continue;
            }

            var plural = keyword + "s";
            if (result.Contains(plural))
            {
                result = result.Replace(plural, $"<style=\"{keyword}\"></style>");
                continue;
            }

            var style = styleSheet.GetStyle(keyword);
            if (result.Contains(keyword))
            {
                result = result.Replace(keyword, $"<style=\"{keyword}\"></style>");
                continue;
            }

            var lowerCase = keyword.ToLower();
            if (result.Contains(lowerCase))
            {
                result = result.Replace(lowerCase, $"<style=\"{keyword}\"></style>");
                continue;
            }
        }

        return result;
    }
}
