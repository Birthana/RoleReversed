using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCard : Card
{
    public Monster monsterPrefab;
    private Transform selectedRoom;

    public override bool HasTarget()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hit = Physics2D.Raycast(ray.origin, Vector2.zero, 100, 1 << LayerMask.NameToLayer("Room"));
        if (hit)
        {
            selectedRoom = hit.transform;
            if (selectedRoom.GetComponent<Room>().HasCapacity(1))
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
