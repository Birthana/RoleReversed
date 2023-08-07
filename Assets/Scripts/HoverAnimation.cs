using System.Collections;
using UnityEngine;

public class HoverAnimation : MonoBehaviour
{
    private Coroutine hoverCoroutine;
    private Coroutine returnCoroutine;
    private ShakeAnimation shakeAnimation;
    private ShakeAnimation returnAnimation;

    public void Hover(Card card, float verticalMove, float time)
    {
        if (hoverCoroutine == null)
        {
            hoverCoroutine = StartCoroutine(Hovering(card, verticalMove, time));
            return;
        }

        StopCoroutine(hoverCoroutine);
        hoverCoroutine = StartCoroutine(Hovering(card, verticalMove, time));
    }

    public void StopHover(Card card)
    {
        returnCoroutine = StartCoroutine(StopHovering(card));
    }

    public bool IsRunning() { return hoverCoroutine != null; }

    private IEnumerator Hovering(Card card, float verticalMove, float time)
    {
        Debug.Log($"Starting: {card.transform.position}");
        if (returnAnimation != null)
        {
            StopCoroutine(returnCoroutine);
            returnAnimation.ReturnToStartPosition();
            returnCoroutine = null;
            returnAnimation = null;
        }

        shakeAnimation = new ShakeAnimation(card.transform, verticalMove, time);
        yield return StartCoroutine(shakeAnimation.AnimateFromStartToEnd());
        hoverCoroutine = null;
    }

    private IEnumerator StopHovering(Card card)
    {
        Debug.Log($"Stopping: {card.transform.position}");
        returnAnimation = shakeAnimation;
        yield return StartCoroutine(shakeAnimation.AnimateFromEndToStart());
    }
}
