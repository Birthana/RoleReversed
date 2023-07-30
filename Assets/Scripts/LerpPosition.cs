using UnityEngine;

public class LerpPosition
{
    private Vector2 startPosition;
    private float verticalMove;

    public LerpPosition(Vector2 startPosition) 
    {
        this.startPosition = startPosition;
    }

    public LerpPosition(Vector2 startPosition, float verticalMove)
    {
        this.startPosition = startPosition;
        this.verticalMove = verticalMove;
    }

    public Vector2 GetStartPosition() { return startPosition; }
    public float GetVerticalMove() { return verticalMove; }

    public Vector2 GetCurrentPosition(float timePercentage)
    {
        var endPosition = startPosition + new Vector2(0, verticalMove);
        return Vector2.Lerp(startPosition, endPosition, timePercentage);
    }
}
