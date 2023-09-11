using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAnimation
{
    private SpriteRenderer renderer;
    private Color endColor;
    private float time;
    private Color startColor;
    private Color currentColor;

    public DamageAnimation(SpriteRenderer renderer, Color endColor, float time)
    {
        this.renderer = renderer;
        this.endColor = endColor;
        this.time = time;
        startColor = renderer.color;
    }

    public IEnumerator AnimateFromStartToEnd()
    {
        var lerp = new LerpColor(startColor, endColor);
        yield return Animate(lerp);
    }

    public IEnumerator AnimateFromEndToStart()
    {
        var lerp = new LerpColor(endColor, startColor);
        yield return Animate(lerp);
    }

    private IEnumerator Animate(LerpColor lerp)
    {
        var currentTime = 0.0f;
        while(currentTime < time)
        {
            currentTime += Time.deltaTime;
            currentColor = lerp.GetCurrentColor(currentTime / time);
            renderer.color = currentColor;
            yield return null;
        }
    }

    public Color GetColor()
    {
        return currentColor;
    }
}
