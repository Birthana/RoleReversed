using UnityEngine;

public class BlockCenterPosition
{
    private Vector3 position;
    private int size;
    private int maxSize;
    private float spacing;
    private float rowColumnSeperation;
    private CenterPosition centerPosition;

    public BlockCenterPosition(Vector3 position, int size, int maxSize, float spacing, float rowColumnSeperation)
    {
        this.position = position;
        this.size = size;
        this.maxSize = maxSize;
        this.spacing = spacing;
        this.rowColumnSeperation = rowColumnSeperation;
        centerPosition = new CenterPosition(position, GetSize(), spacing);
    }

    private int GetSize()
    {
        if (size < maxSize)
        {
            return size;
        }

        return maxSize;
    }

    public Vector3 GetHorizontalLayoutPositionAt(int index)
    {
        var position = centerPosition.GetHorizontalOffsetPositionAt(index % maxSize);
        return new Vector3(position.x, GetVerticalPosition(index), position.z);
    }

    private float GetVerticalPosition(int index)
    {
        int row = index / maxSize;
        return position.x - (row * rowColumnSeperation);
    }
    public Vector3 GetVerticalLayoutPositionAt(int index)
    {
        var position = centerPosition.GetVerticalOffsetPositionAt(index % maxSize);
        return new Vector3(GetHorizontalPosition(index), position.y, position.z);
    }

    private float GetHorizontalPosition(int index)
    {
        int column = index / maxSize;
        return position.x + (column * rowColumnSeperation);
    }

}
