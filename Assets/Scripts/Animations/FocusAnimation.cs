using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusAnimation : MonoBehaviour
{
    private static readonly float FOCUS_TIME = 0.25f;
    private Vector3 focusPosition;
    private float focusScale;
    private ShakeAnimation moveAnimation;
    private ScaleAnimation scaleAnimation;
    private Transform previousTransform;

    public void SetFocusPosition(Vector3 position)
    {
        focusPosition = position;
    }

    public void SetFocusScale(float scale)
    {
        focusScale = scale;
    }

    public void Focus(Transform transformToFocus)
    {
        StartCoroutine(FocusOn(transformToFocus));
    }

    public IEnumerator FocusOn(Transform transformToFocus)
    {
        if (previousTransform != null)
        {
            yield return UnfocusOn();
        }

        yield return MoveToPosition(transformToFocus, focusPosition);
        yield return ScaleUp(transformToFocus, focusScale);
        previousTransform = transformToFocus;
    }

    private IEnumerator MoveToPosition(Transform transformToFocus, Vector3 positon)
    {
        var distance = positon - transformToFocus.position;
        moveAnimation = new ShakeAnimation(transformToFocus, distance, FOCUS_TIME);
        yield return moveAnimation.AnimateFromStartToEnd();
    }

    private IEnumerator ScaleUp(Transform transformToFocus, float scale)
    {
        scaleAnimation = new ScaleAnimation(transformToFocus, scale, FOCUS_TIME);
        yield return scaleAnimation.AnimateStartToEnd();
    }

    public IEnumerator UnfocusOn()
    {
        if (moveAnimation == null || scaleAnimation == null || previousTransform == null)
        {
            yield break;
        }

        yield return scaleAnimation.AnimateEndToStart();
        yield return moveAnimation.AnimateFromEndToStart();
        previousTransform = null;
    }
}
