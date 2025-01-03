using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionRoom : Room
{
    private Timer timer;

    private Timer GetTimerComponent()
    {
        if (timer == null)
        {
            timer = GetComponent<Timer>();
        }

        return timer;
    }

    public override void Setup(RoomCardInfo roomCardInfo, Vector3 position)
    {
        base.Setup(roomCardInfo, position);
        SetTimer(((ConstructionRoomCardInfo)roomCardInfo).GetTimer());
    }

    public void SetTimer(int timer)
    {
        GetTimerComponent().maxCount = timer;
        GetTimerComponent().ResetMaxTimer();
    }

    public void ReduceTimer()
    {
        GetTimerComponent().DecreaseTimer(monsters.Count);
    }

    public bool CanBeBuilt()
    {
        return GetTimerComponent().GetCount() == 0;
    }

    public void SpawnRoom()
    {
        var fieldRoom = Resources.Load<Room>(RoomCardInfo.FIELD_ROOM_PREFAB);
        var room = Instantiate(fieldRoom);
        var constructionRoomCardInfo = (ConstructionRoomCardInfo)cardInfo;
        room.Setup(constructionRoomCardInfo.roomCardInfo, transform.position);

        if (monsters.Count != 0)
        {
            foreach(var monster in monsters)
            {
                monster.transform.SetParent(room.transform);
                room.Enter(monster);
                monster.Unlock();
            }
        }

        var gameManager = FindObjectOfType<GameManager>();
        gameManager.RemoveRoom(this);
        gameManager.AddToRooms(room);
    }
}
