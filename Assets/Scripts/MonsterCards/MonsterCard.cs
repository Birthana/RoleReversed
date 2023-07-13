using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCard : Card
{
    public Monster monsterPrefab;
    private Transform selectedRoom;

    public override bool HasTarget()
    {
        if (Mouse.IsOnRoom())
        {
            selectedRoom = Mouse.GetHitTransform();
            if (selectedRoom.GetComponent<Room>().HasCapacity())
            {
                return true;
            }
        }

        return false;
    }

    public override void Cast()
    {
        SpawnMonster();
        base.Cast();
    }

    private void SpawnMonster()
    {
        var monster = Instantiate(monsterPrefab, selectedRoom);
        var room = selectedRoom.GetComponent<Room>();
        room.Add(monster);
        room.ReduceCapacity(1);
    }
}
