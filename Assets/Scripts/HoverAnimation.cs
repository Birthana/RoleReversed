using System.Collections;
using UnityEngine;

public class HoverAnimation : MonoBehaviour
{
    private Coroutine hoverCoroutine;
    private Coroutine returnCoroutine;
    private ShakeAnimation shakeAnimation;

    public void Hover(Card card, Vector2 movement, float time)
    {
        Hover(card.transform, movement, time);
    }

    public void Hover(Transform objectTransform, Vector2 movement, float time)
    {
        if (IsHovering())
        {
            StopHover();
        }

        PerformHover(objectTransform, movement, time);
    }

    public void PerformReturn()
    {
        returnCoroutine = StartCoroutine(Returning());
    }

    public bool IsRunning() { return IsHovering() || IsReturning(); }

    public void ResetHoverAnimation()
    {
        if (IsHovering())
        {
            StopHover();
        }

        if (IsReturning())
        {
            StopReturn();
        }
    }

    private IEnumerator Hovering(Transform objectTransform, Vector2 movement, float time)
    {
        if (IsRunning())
        {
            StopReturn();
            shakeAnimation.ReturnToStartPosition();
        }

        shakeAnimation = new ShakeAnimation(objectTransform, movement, time);
        yield return StartCoroutine(shakeAnimation.AnimateFromStartToEnd());
        hoverCoroutine = null;
    }

    private IEnumerator Returning()
    {
        yield return StartCoroutine(shakeAnimation.AnimateFromEndToStart());
    }

    private bool IsNotHovering() { return hoverCoroutine == null; }

    private bool IsHovering() { return !IsNotHovering(); }

    private bool IsReturning() { return returnCoroutine != null; }

    private void PerformHover(Transform objectTransform, Vector2 movement, float time) { hoverCoroutine = StartCoroutine(Hovering(objectTransform, movement, time)); }

    private void StopHover()
    {
        StopCoroutine(hoverCoroutine);
        hoverCoroutine = null;
    }

    private void StopReturn()
    {
        StopCoroutine(returnCoroutine);
        returnCoroutine = null;
    }
}
