using System.Collections;
using UnityEngine;

public class ShakeAnimation
{
    private Transform transform;
    private Vector2 movement;
    private float animationTime;

    private Vector2 currentPosition;
    private Vector2 startPosition;
    private Vector2 differenceMovement;

    public ShakeAnimation(Transform transform, Vector2 movement, float animationTime)
    {
        this.transform = transform;
        this.movement = movement;
        this.animationTime = animationTime;
        startPosition = transform.position;
        currentPosition = startPosition;
    }

    public float GetAnimationTime() { return animationTime; }
    public Vector2 GetPosition() { return currentPosition; }

    public IEnumerator AnimateFromStartToEnd()
    {
        var startPositionLerp = new LerpPosition(transform.position, movement);
        yield return Animate(startPositionLerp);
    }

    public IEnumerator AnimateFromEndToStart()
    {
        if(startPosition == (Vector2)transform.position)
        {
            yield break;
        }

        var endPositionLerp = new LerpPosition(currentPosition, -differenceMovement);
        yield return Animate(endPositionLerp);
    }

    public void ReturnToStartPosition()
    {
        transform.position = startPosition;
    }

    private IEnumerator Animate(LerpPosition lerp)
    {
        var currentTime = 0.0f;
        differenceMovement = new Vector2(0, 0);
        while (currentTime < animationTime)
        {
            currentTime += Time.deltaTime;
            currentPosition = lerp.GetCurrentPosition(currentTime / animationTime);
            if (transform == null)
            {
                yield break;
            }

            transform.position = currentPosition;
            SetDifference(lerp);
            yield return null;
        }
    }

    private void SetDifference(LerpPosition lerp)
    {
        var differenceX = currentPosition.x - lerp.GetStartPosition().x;
        var differenceY = currentPosition.y - lerp.GetStartPosition().y;
        differenceMovement = new Vector2(differenceX, differenceY);
    }
}
