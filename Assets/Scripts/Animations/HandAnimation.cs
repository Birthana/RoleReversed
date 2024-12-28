using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct HandMoveInfo
{
    public Card card;
    public Vector3 startPosition;
    public Vector3 endPosition;

    public HandMoveInfo(Card card, Vector3 startPosition, Vector3 endPosition)
    {
        this.card = card;
        this.startPosition = startPosition;
        this.endPosition = endPosition;
    }
}

public class HandAnimation : MonoBehaviour
{
    public List<HandMoveInfo> queue = new List<HandMoveInfo>();
    private Coroutine coroutine;

    private void Update()
    {
        if (queue.Count == 0 || IsActive())
        {
            return;
        }

        MoveCard(queue[0].card, queue[0].startPosition, queue[0].endPosition);
        queue.RemoveAt(0);
    }

    private bool IsActive() { return coroutine != null; }

    public void MoveCard(Card card, Vector3 startPosition, Vector3 endPosition)
    {
        if (IsActive())
        {
            queue.Add(new HandMoveInfo(card, startPosition, endPosition));
            return;
        }

        coroutine = StartCoroutine(Move(card, card.transform.position, endPosition));
    }

    private IEnumerator Move(Card card, Vector3 startPosition, Vector3 endPosition)
    {
        var distanceToTravel = endPosition - startPosition;
        var shakeAnimation = new ShakeAnimation(card.transform, distanceToTravel, 0.1f);
        yield return shakeAnimation.AnimateFromStartToEnd();
        coroutine = null;
    }
}
