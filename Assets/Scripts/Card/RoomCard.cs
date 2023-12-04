using System.Collections.Generic;
using UnityEngine;

public class RoomCard : Card
{
    private static readonly string FIELD_ROOM_PREFAB = "Prefabs/FieldRoom";
    private Room roomPrefab;
    private GameManager gameManager;
    private RoomTransform roomTransform;
    private RoomCardInfo roomCardInfo;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public override void SetCardInfo(CardInfo newCardInfo)
    {
        roomPrefab = Resources.Load<Room>(FIELD_ROOM_PREFAB);
        roomCardInfo = (RoomCardInfo)newCardInfo;
        base.SetCardInfo(newCardInfo);
    }

    public override bool HasTarget()
    {
        if (Mouse.IsOnSpace())
        {
            roomTransform = new RoomTransform(Mouse.GetHitTransform());
            if (!RoomIsAdjacentToRoom())
            {
                return false;
            }

            return true;
        }

        return false;
    }

    public bool RoomIsAdjacentToRoom()
    {
        if (gameManager.DoesNotHaveStartRoom())
        {
            return true;
        }

        return roomTransform.SelectSpaceHasAdjacentRoom();
    }

    public override void Cast()
    {
        SpawnRoom();
        base.Cast();
    }

    private void SpawnRoom()
    {
        var room = Instantiate(roomPrefab);
        room.GetComponent<SpriteRenderer>().sprite = roomCardInfo.fieldSprite;
        room.SetCapacity(roomCardInfo.capacity);
        room.transform.position = roomTransform.GetTransform().position;
        Destroy(roomTransform.GetTransform().gameObject);
        if (gameManager.DoesNotHaveStartRoom())
        {
            gameManager.SetStartRoom(room);
        }
    }
}
