using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpScale
{
    private float startScale;
    private float endScale;

    public LerpScale(float startScale, float endScale)
    {
        this.startScale = startScale;
        this.endScale = endScale;
    }

    public float GetCurrentScale(float timePercentage)
    {
        return Mathf.Lerp(startScale, endScale, timePercentage);
    }
}
