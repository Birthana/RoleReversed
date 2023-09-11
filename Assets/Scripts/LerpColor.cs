using UnityEngine;

public class LerpColor
{
    public Color startColor;
    public Color endColor;

    public LerpColor(Color start, Color end)
    {
        startColor = start;
        endColor = end;
    }

    public Color GetCurrentColor(float timePercentage)
    {
        return Color.Lerp(startColor, endColor, timePercentage);
    }
}
