using System.Collections.Generic;
using UnityEngine;

public class RoomCard : Card
{
    public Room roomPrefab;
    private GameManager gameManager;
    private RoomTransform roomTransform;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public override void SetCardInfo(CardInfo newCardInfo)
    {
        roomPrefab = newCardInfo.prefab.GetComponent<Room>();
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
        room.transform.position = roomTransform.GetTransform().position;
        Destroy(roomTransform.GetTransform().gameObject);
        if (gameManager.DoesNotHaveStartRoom())
        {
            gameManager.SetStartRoom(room);
        }
    }
}
