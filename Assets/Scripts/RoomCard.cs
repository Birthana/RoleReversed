using System.Collections.Generic;
using UnityEngine;

public class RoomCard : Card
{
    public Room roomPrefab;
    private Transform selectedSpace;
    public float HORIZONTAL_SPACING = 5.0f;
    public float VERTICAL_SPACING = 5.0f;

    public override bool HasTarget()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hit = Physics2D.Raycast(ray.origin, Vector2.zero, 100, 1 << LayerMask.NameToLayer("Space"));
        if (hit)
        {
            if (!IsAdjacentToRoom(hit.transform))
            {
                return false;
            }

            selectedSpace = hit.transform;
            return true;
        }

        return false;
    }

    private bool IsAdjacentToRoom(Transform selectedTransform)
    {
        var gameManager = FindObjectOfType<GameManager>();
        if (gameManager.DoesNotHaveStartRoom())
        {
            return true;
        }

        return GetAdjacentRooms(selectedTransform).Count != 0;
    }

    private List<Room> GetAdjacentRooms(Transform selectedTransform)
    {
        var adjacentRooms = new List<Room>();
        foreach (var position in GetAdjacentPositions(selectedTransform))
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

    private List<Vector3> GetAdjacentPositions(Transform selectedTransform)
    {
        var adjacentPositions = new List<Vector3>();
        adjacentPositions.Add(selectedTransform.position + new Vector3(HORIZONTAL_SPACING, 0, -10));
        adjacentPositions.Add(selectedTransform.position + new Vector3(-HORIZONTAL_SPACING, 0, -10));
        adjacentPositions.Add(selectedTransform.position + new Vector3(0, VERTICAL_SPACING, -10));
        adjacentPositions.Add(selectedTransform.position + new Vector3(0, -VERTICAL_SPACING, -10));
        return adjacentPositions;
    }


    public override void Cast()
    {
        SpawnRoom();
        base.Cast();
    }

    private void SpawnRoom()
    {
        var room = Instantiate(roomPrefab);
        room.transform.position = selectedSpace.position;
        Destroy(selectedSpace.gameObject);
        var gameManager = FindObjectOfType<GameManager>();
        if (gameManager.DoesNotHaveStartRoom())
        {
            gameManager.SetStartRoom(room);
        }
    }
}
