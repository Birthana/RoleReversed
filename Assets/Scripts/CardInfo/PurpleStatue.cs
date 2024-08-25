using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PurpleStatue", menuName = "CardInfo/PurpleStatue")]
public class PurpleStatue : MonsterCardInfo
{
    public override void Exit(Monster self)
    {
        var parentRoom = self.GetCurrentRoom();
        var adjacentRooms = parentRoom.GetAdjacentRooms();
        if (adjacentRooms.Count == 0)
        {
            return;
        }

        self.SpawnExitIcon();
        PullRandomAdjacentRoomMonster(parentRoom, adjacentRooms);
    }

    private void PullRandomAdjacentRoomMonster(Room currentRoom, List<Room> adjacentRooms)
    {
        var roomMonster = GetRandomRoomMonster(adjacentRooms);
        if (roomMonster == null)
        {
            return;
        }

        roomMonster.Pull(currentRoom);
        new ChangeSortingLayer(roomMonster.gameObject).SetToCurrentRoom();
    }

    private Monster GetRandomRoomMonster(List<Room> adjacentRooms)
    {
        return new RandomMonster(adjacentRooms).Get();
    }
}

