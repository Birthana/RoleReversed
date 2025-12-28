using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour
{
    public List<Color> colors = new List<Color>();
    private List<Color> unUsedColors = new List<Color>();

    private void Awake()
    {
        LoadAllColors();
    }

    private void LoadAllColors()
    {
        foreach (var color in colors)
        {
            unUsedColors.Add(color);
        }
    }

    public Color GetRandomColor()
    {
        if (unUsedColors.Count == 0)
        {
            LoadAllColors();
        }

        var rngIndex = Random.Range(0, unUsedColors.Count);
        var color  = unUsedColors[rngIndex];
        unUsedColors.RemoveAt(rngIndex);

        return color;
    }
}
