using System.Collections.Generic;
using UnityEngine;

public class RoomCard : Card
{
    public Room roomPrefab;
    private Transform selectedSpace;
    public float HORIZONTAL_SPACING = 5.0f;
    public float VERTICAL_SPACING = 5.0f;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public override bool HasTarget()
    {
        if (Mouse.IsOnSpace())
        {
            if (!IsAdjacentToRoom(Mouse.GetHitTransform()))
            {
                return false;
            }

            selectedSpace = Mouse.GetHitTransform();
            return true;
        }

        return false;
    }

    public override void Cast()
    {
        SpawnRoom();
        base.Cast();
    }

    private bool IsAdjacentToRoom(Transform selectedTransform)
    {
        if (gameManager.DoesNotHaveStartRoom())
        {
            return true;
        }

        return SelectSpaceHasAdjacentRoom(selectedTransform);
    }

    private bool SelectSpaceHasAdjacentRoom(Transform selectedTransform) { return GetAdjacentRooms(selectedTransform).Count != 0; }

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

    private void SpawnRoom()
    {
        var room = Instantiate(roomPrefab);
        room.transform.position = selectedSpace.position;
        Destroy(selectedSpace.gameObject);
        if (gameManager.DoesNotHaveStartRoom())
        {
            gameManager.SetStartRoom(room);
        }
    }
}
