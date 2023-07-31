using System.Collections;
using UnityEngine;

public class OnHover : MonoBehaviour
{
    private ShakeAnimation shakeAnimation;
    private CardDragger cardDragger;
    private bool hovering = false;
    private Coroutine coroutine;

    private void Start()
    {
        shakeAnimation = new ShakeAnimation(transform, 0.5f, 0.1f);
        cardDragger = FindObjectOfType<CardDragger>();
    }

    //private void Update()
    //{
    //    if (Mouse.IsOnHand())
    //    {
    //        if (Mouse.GetHitTransform().gameObject.Equals(gameObject))
    //        {
    //            if (cardDragger.GetSelectedCard() == null && !hovering && coroutine == null)
    //            {
    //                hovering = true;
    //                coroutine = StartCoroutine(Hover());
    //            }
    //        }
    //    }
    //}

    //private IEnumerator Hover()
    //{
    //    yield return StartCoroutine(shakeAnimation.AnimateFromStartToEnd());
    //    bool stillRunning = true;
    //    while (stillRunning)
    //    {
    //        if (!Mouse.IsOnHand() && cardDragger.GetSelectedCard() == null && hovering)
    //        {
    //            yield return StartCoroutine(shakeAnimation.AnimateFromEndToStart());
    //            hovering = false;
    //            stillRunning = false;
    //        }

    //        yield return null;
    //    }

    //    coroutine = null;
    //}
}
