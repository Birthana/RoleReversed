using System.Collections.Generic;
using UnityEngine;

public class RandomMonster
{
    private List<Room> rooms;

    public RandomMonster(List<Room> rooms)
    {
        this.rooms = rooms;
    }

    public Monster Get()
    {
        var roomMonsters = GetRoomMonsters();
        if (roomMonsters.Count == 0)
        {
            return null;
        }

        return roomMonsters[Random.Range(0, roomMonsters.Count)];
    }

    private List<Monster> GetRoomMonsters()
    {
        var roomMonsters = new List<Monster>();
        foreach (var room in rooms)
        {
            foreach (var monster in room.monsters)
            {
                if (monster.IsDead() || monster.GetCurrentRoom() != room)
                {
                    continue;
                }

                roomMonsters.Add(monster);
            }
        }

        return roomMonsters;
    }
}
