using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHover : MonoBehaviour
{
    private ShakeAnimation shakeAnimation;

    private void Start()
    {
        shakeAnimation = new ShakeAnimation(transform, 0.5f, 0.1f);
    }

    private void OnMouseEnter()
    {
        StartCoroutine(shakeAnimation.AnimateFromStartToEnd());
    }

    private void OnMouseExit()
    {
        StartCoroutine(shakeAnimation.AnimateFromEndToStart());
    }
}
