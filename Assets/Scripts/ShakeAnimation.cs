using System.Collections;
using UnityEngine;

public class ShakeAnimation
{
    private LerpPosition startPositionLerp;
    private LerpPosition endPositionLerp;
    private float animationTime;
    private Vector2 currentPosition;
    private Transform transform;

    public ShakeAnimation(float animationTime)
    {
        startPositionLerp = new LerpPosition(Vector2.zero);
        endPositionLerp = new LerpPosition(Vector2.zero);
        this.animationTime = animationTime;
    }

    public ShakeAnimation(Vector2 startPosition, float verticalMove, float animationTime)
    {
        startPositionLerp = new LerpPosition(startPosition, verticalMove);
        endPositionLerp = new LerpPosition(startPosition + new Vector2(0, verticalMove), -verticalMove);
        this.animationTime = animationTime;
    }

    public ShakeAnimation(Transform transform, float verticalMove, float animationTime)
    {
        this.transform = transform;
        Vector2 position = transform.position;
        startPositionLerp = new LerpPosition(position, verticalMove);
        endPositionLerp = new LerpPosition(position + new Vector2(0, verticalMove), -verticalMove);
        this.animationTime = animationTime;
    }

    public float GetAnimationTime() { return animationTime; }
    public Vector2 GetPosition() { return currentPosition; }
    public IEnumerator AnimateFromStartToEnd() { yield return Animate(startPositionLerp); }
    public IEnumerator AnimateFromEndToStart() { yield return Animate(endPositionLerp); }

    private IEnumerator Animate(LerpPosition lerp)
    {
        var currentTime = 0.0f;
        while (currentTime < animationTime)
        {
            currentTime += Time.deltaTime;
            currentPosition = lerp.GetCurrentPosition(currentTime / animationTime);

            if (transform != null)
            {
                transform.position = currentPosition;
            }

            yield return null;
        }
    }
}
