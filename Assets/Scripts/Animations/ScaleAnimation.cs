using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleAnimation
{
    private Transform transformToScale;
    private float finalScale;
    private float time;

    public ScaleAnimation(Transform transform, float scale, float time)
    {
        transformToScale = transform;
        finalScale = scale;
        this.time = time;
    }

    public float GetCurrentScale() { return transformToScale.localScale.x; }

    public IEnumerator AnimateStartToEnd()
    {
        var lerpScale = new LerpScale(1, finalScale);
        yield return Animate(lerpScale);
    }

    public IEnumerator AnimateEndToStart()
    {
        var lerpScale = new LerpScale(finalScale, 1);
        yield return Animate(lerpScale);
    }

    private IEnumerator Animate(LerpScale lerp)
    {
        var currentTime = 0.0f;
        while (currentTime < time)
        {
            currentTime += Time.deltaTime;
            var scale = lerp.GetCurrentScale(currentTime / time);
            transformToScale.localScale = new Vector3(scale, scale, 1);
            yield return null;
        }
    }
}
