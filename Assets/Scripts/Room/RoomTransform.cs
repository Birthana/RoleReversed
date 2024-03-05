using System.Collections.Generic;
using UnityEngine;

public class RoomTransform
{
    private Transform selectedTransform;
    private float horizontalSpacing;
    private float verticalSpacing;

    public RoomTransform()
    {
        horizontalSpacing = 5.0f;
        verticalSpacing = 5.0f;
    }

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
        return GetAdjacentRooms(selectedTransform.position);
    }

    public List<Room> GetAdjacentRooms(Vector3 centerPosition)
    {
        var adjacentRooms = new List<Room>();
        foreach (var position in GetAdjacentPositions(centerPosition))
        {
            var ray = Camera.main.ScreenPointToRay(Camera.main.WorldToScreenPoint(position));
            var hits = Physics2D.RaycastAll(ray.origin, Vector2.zero, 100, 1 << LayerMask.NameToLayer("Room"));
            if (hits.Length == 0)
            {
                continue;
            }

            var room = IgnoreCurrentFocusedRoom(hits);
            if (room == null)
            {
                continue;
            }

            adjacentRooms.Add(room);
        }

        return adjacentRooms;
    }

    private Room IgnoreCurrentFocusedRoom(RaycastHit2D[] hits)
    {
        foreach (var room in hits)
        {
            if (room.transform.GetComponent<SpriteRenderer>().sortingLayerName == "CurrentRoom")
            {
                continue;
            }

            return room.transform.GetComponent<Room>();
        }

        return null;
    }

    private List<Vector3> GetAdjacentPositions(Vector3 position)
    {
        var adjacentPositions = new List<Vector3>();
        adjacentPositions.Add(position + new Vector3(horizontalSpacing, 0, -10));
        adjacentPositions.Add(position + new Vector3(-horizontalSpacing, 0, -10));
        adjacentPositions.Add(position + new Vector3(0, verticalSpacing, -10));
        adjacentPositions.Add(position + new Vector3(0, -verticalSpacing, -10));
        return adjacentPositions;
    }
}
