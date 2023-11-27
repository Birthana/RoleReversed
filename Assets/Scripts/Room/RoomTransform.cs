using System.Collections.Generic;
using UnityEngine;

public class RoomTransform
{
    private Transform selectedTransform;
    private float horizontalSpacing;
    private float verticalSpacing;

    public RoomTransform(Transform selectedTransform)
    {
        this.selectedTransform = selectedTransform;
        horizontalSpacing = 5.0f;
        verticalSpacing = 5.0f;
    }

    public Transform GetTransform() { return selectedTransform; }

    public bool SelectSpaceHasAdjacentRoom() { return GetAdjacentRooms().Count != 0; }

    public List<Room> GetAdjacentRooms()
    {
        var adjacentRooms = new List<Room>();
        foreach (var position in GetAdjacentPositions())
        {
            var ray = Camera.main.ScreenPointToRay(Camera.main.WorldToScreenPoint(position));
            var hit = Physics2D.Raycast(ray.origin, Vector2.zero, 100, 1 << LayerMask.NameToLayer("Room"));
            if (hit)
            {
                adjacentRooms.Add(hit.transform.GetComponent<Room>());
            }
        }

        return adjacentRooms;
    }

    private List<Vector3> GetAdjacentPositions()
    {
        var adjacentPositions = new List<Vector3>();
        adjacentPositions.Add(selectedTransform.position + new Vector3(horizontalSpacing, 0, -10));
        adjacentPositions.Add(selectedTransform.position + new Vector3(-horizontalSpacing, 0, -10));
        adjacentPositions.Add(selectedTransform.position + new Vector3(0, verticalSpacing, -10));
        adjacentPositions.Add(selectedTransform.position + new Vector3(0, -verticalSpacing, -10));
        return adjacentPositions;
    }
}
