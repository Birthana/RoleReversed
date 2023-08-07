using System.Collections;
using UnityEngine;

public class ShakeAnimation
{
    private Transform transform;
    private float verticalMove;
    private float animationTime;
    private Vector2 currentPosition;
    private Vector2 startPosition;
    private float difference;

    public ShakeAnimation(Transform transform, float verticalMove, float animationTime)
    {
        this.transform = transform;
        this.verticalMove = verticalMove;
        this.animationTime = animationTime;
        startPosition = transform.position;
        currentPosition = startPosition;
        difference = 0.0f;
    }

    public float GetAnimationTime() { return animationTime; }
    public Vector2 GetPosition() { return currentPosition; }

    public IEnumerator AnimateFromStartToEnd()
    {
        var startPositionLerp = new LerpPosition(transform.position, verticalMove);
        yield return Animate(startPositionLerp);
    }

    public IEnumerator AnimateFromEndToStart()
    {
        if(startPosition == (Vector2)transform.position)
        {
            yield break;
        }

        var endPositionLerp = new LerpPosition(currentPosition, -difference);
        yield return Animate(endPositionLerp);
    }

    public void ReturnToStartPosition()
    {
        Debug.Log($"Returning: {startPosition}");
        transform.position = startPosition;
    }

    private IEnumerator Animate(LerpPosition lerp)
    {
        var currentTime = 0.0f;
        difference = 0.0f;
        while (currentTime < animationTime)
        {
            currentTime += Time.deltaTime;
            currentPosition = lerp.GetCurrentPosition(currentTime / animationTime);
            transform.position = currentPosition;
            difference = Mathf.Abs(currentPosition.y - lerp.GetStartPosition().y);
            yield return null;
        }
    }
}
