using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MoveInfo
{
    public Monster monster;
    public Room endRoom;
    public Action<Room> func;

    public MoveInfo(Monster monster, Room endRoom, Action<Room> func)
    {
        this.monster = monster;
        this.endRoom = endRoom;
        this.func = func;
    }
}

public class MoveAnimation : MonoBehaviour
{
    public List<MoveInfo> queue = new List<MoveInfo>();
    private Coroutine coroutine;

    private void Update()
    {
        if (queue.Count == 0 || coroutine != null)
        {
            return;
        }

        MoveMonster(queue[0].monster, queue[0].endRoom, queue[0].func);
        queue.RemoveAt(0);
    }

    public void MoveMonster(Monster monster, Room newRoom, Action<Room> func)
    {
        if (coroutine != null)
        {
            queue.Add(new MoveInfo(monster, newRoom, func));
        }

        coroutine = StartCoroutine(Move(monster, monster.GetCurrentRoom(), newRoom, func));
    }

    private IEnumerator Move(Monster monster, Room startRoom, Room endRoom, Action<Room> func)
    {
        var moveLine = monster.GetComponent<MoveLine>();
        moveLine.SetStartPoint();
        moveLine.Enable();
        var distanceToTravel = endRoom.transform.position - startRoom.transform.position;
        var shakeAnimation = new ShakeAnimation(monster.transform, distanceToTravel, 0.3f);
        yield return shakeAnimation.AnimateFromStartToEnd();
        func(endRoom);
        moveLine.Disable();
        coroutine = null;
    }
}
