using UnityEngine;

public class CenterPosition
{
    private Vector3 position;
    private int size;
    private float spacing;

    public CenterPosition(Vector3 position, int size, float spacing)
    {
        this.position = position;
        this.size = size;
        this.spacing = spacing;
    }

    public Vector3 GetCenterPosition() { return position; }

    public int GetSize() { return size; }

    public Vector3 GetHorizontalOffsetPositionAt(int index)
    {
        float x = position.x + GetOffset(index);
        return new Vector3(x, position.y, position.z);
    }

    public Vector3 GetVerticalOffsetPositionAt(int index)
    {
        float y = position.y + GetOffset(index);
        return new Vector3(position.x, y, position.z);
    }

    private float GetOffset(int index)
    {
        float positionOffset = index - ((float)size - 1) / 2;
        return Mathf.Sin(positionOffset * Mathf.Deg2Rad) * spacing * 10;
    }
}
