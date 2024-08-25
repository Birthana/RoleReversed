using System.Collections.Generic;
using UnityEngine;

public class RoomTransform
{
    private Transform selectedTransform;
    private float horizontalSpacing;
    private float verticalSpacing;

    public RoomTransform()
    {
        horizontalSpacing = SpaceManager.HORIZONTAL_SPACING;
        verticalSpacing = SpaceManager.VERTICAL_SPACING;
    }

    public RoomTransform(Transform selectedTransform)
    {
        this.selectedTransform = selectedTransform;
        horizontalSpacing = SpaceManager.HORIZONTAL_SPACING;
        verticalSpacing = SpaceManager.VERTICAL_SPACING;
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
            var room = GetRoomAt(position);
            if (room == null)
            {
                continue;
            }

            adjacentRooms.Add(room);
        }

        return adjacentRooms;
    }

    private bool DoesNotContainsRoomAt(Vector3 position) { return GetHits(position).Length == 0; }

    private RaycastHit2D[] GetHits(Vector3 position)
    {
        var ray = Camera.main.ScreenPointToRay(Camera.main.WorldToScreenPoint(position));
        return Physics2D.RaycastAll(ray.origin, Vector2.zero, 100, 1 << LayerMask.NameToLayer("Room"));
    }

    private Room GetRoomAt(Vector3 position)
    {
        if (DoesNotContainsRoomAt(position))
        {
            return null;
        }

        return IgnoreCurrentFocusedRoom(GetHits(position));
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
