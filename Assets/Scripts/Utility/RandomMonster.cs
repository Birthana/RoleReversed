using System.Collections.Generic;
using UnityEngine;

public class RandomMonster
{
    private List<Room> rooms;
    private Monster roomMonster;

    public RandomMonster(List<Room> rooms)
    {
        this.rooms = rooms;
        roomMonster = null;
        FindMonster();
    }

    public Monster Get() { return roomMonster; }

    private void FindMonster()
    {
        while (StillLooking())
        {
            roomMonster = GetRandomRoom().GetRandomMonster();
        }
    }

    private bool StillLooking() { return HasRooms() && MonsterIsNotFound(); }

    private bool HasRooms() { return rooms.Count != 0; }

    private bool MonsterIsNotFound() { return roomMonster == null; }

    private Room GetRandomRoom()
    {
        var nextRoom = rooms[Random.Range(0, rooms.Count)];
        rooms.Remove(nextRoom);
        return nextRoom;
    }
}
