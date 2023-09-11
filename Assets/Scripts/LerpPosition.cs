using UnityEngine;

public class LerpPosition
{
    private Vector2 startPosition;
    private float horizontalMove;
    private float verticalMove;

    public LerpPosition(Vector2 startPosition) 
    {
        this.startPosition = startPosition;
    }

    public LerpPosition(Vector2 startPosition, Vector2 movement)
    {
        this.startPosition = startPosition;
        horizontalMove = movement.x;
        verticalMove = movement.y;
    }

    public Vector2 GetStartPosition() { return startPosition; }
    public float GetHorizontalMove() { return horizontalMove; }
    public float GetVerticalMove() { return verticalMove; }

    public Vector2 GetCurrentPosition(float timePercentage)
    {
        var endPosition = startPosition + new Vector2(horizontalMove, verticalMove);
        return Vector2.Lerp(startPosition, endPosition, timePercentage);
    }
}
