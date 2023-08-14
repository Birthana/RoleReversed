using System.Collections;
using UnityEngine;

public class HoverAnimation : MonoBehaviour
{
    private Coroutine hoverCoroutine;
    private Coroutine returnCoroutine;
    private ShakeAnimation verticalAnimation;

    public void Hover(Card card, float verticalMove, float time)
    {
        if (IsHovering())
        {
            StopHover();
        }

        PerformHover(card, verticalMove, time);
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

    private IEnumerator Hovering(Card card, float verticalMove, float time)
    {
        if (IsRunning())
        {
            StopReturn();
            verticalAnimation.ReturnToStartPosition();
        }

        verticalAnimation = new ShakeAnimation(card.transform, verticalMove, time);
        yield return StartCoroutine(verticalAnimation.AnimateFromStartToEnd());
        hoverCoroutine = null;
    }

    private IEnumerator Returning()
    {
        yield return StartCoroutine(verticalAnimation.AnimateFromEndToStart());
    }

    private bool IsNotHovering() { return hoverCoroutine == null; }

    private bool IsHovering() { return !IsNotHovering(); }

    private bool IsReturning() { return returnCoroutine != null; }

    private void PerformHover(Card card, float verticalMove, float time) { hoverCoroutine = StartCoroutine(Hovering(card, verticalMove, time)); }

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
