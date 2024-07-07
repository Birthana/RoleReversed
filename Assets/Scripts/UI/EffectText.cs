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
        styleSheet = Resources.Load<TMP_StyleSheet>(EFFECT_TEXT_ASSET_FILE_PATH);
        var result = text;

        foreach (var keyword in keywords)
        {
            if (styleSheet.GetStyle(keyword) == null)
            {
                continue;
            }

            result = GetStyleText(result, keyword);
        }

        return result;
    }

    private string GetStyleText(string text, string keyword)
    {
        var plural = keyword + "s";
        if (text.Contains(plural))
        {
            return ReplaceWordWithStyleText(text, plural, keyword);
        }

        if (text.Contains(keyword))
        {
            return ReplaceWordWithStyleText(text, keyword, keyword);
        }

        var lowerCase = keyword.ToLower();
        if (text.Contains(lowerCase))
        {
            return ReplaceWordWithStyleText(text, lowerCase, keyword);
        }

        return text;
    }

    private string ReplaceWordWithStyleText(string text, string wordToReplace, string stylename)
    {
        return text.Replace(wordToReplace, $"<style=\"{stylename}\"></style>");
    }
}
