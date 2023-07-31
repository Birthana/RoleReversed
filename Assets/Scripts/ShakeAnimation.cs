using System.Collections;
using UnityEngine;

public class ShakeAnimation
{
    private Transform transform;
    private float verticalMove;
    private float animationTime;
    private Vector2 currentPosition;
    private Vector2 startPosition;

    public ShakeAnimation(Transform transform, float verticalMove, float animationTime)
    {
        this.transform = transform;
        this.verticalMove = verticalMove;
        this.animationTime = animationTime;
        startPosition = transform.position;
    }

    public float GetAnimationTime() { return animationTime; }
    public Vector2 GetPosition() { return currentPosition; }

    public IEnumerator AnimateFromStartToEnd()
    {
        startPosition = transform.position;
        var startPositionLerp = new LerpPosition(transform.position, verticalMove);
        yield return Animate(startPositionLerp);
    }

    public IEnumerator AnimateFromEndToStart()
    {
        var endPositionLerp = new LerpPosition(startPosition + new Vector2(0, verticalMove), -verticalMove);
        yield return Animate(endPositionLerp);
    }

    private IEnumerator Animate(LerpPosition lerp)
    {
        var currentTime = 0.0f;
        while (currentTime < animationTime)
        {
            currentTime += Time.deltaTime;
            currentPosition = lerp.GetCurrentPosition(currentTime / animationTime);
            transform.position = currentPosition;
            yield return null;
        }
    }
}
