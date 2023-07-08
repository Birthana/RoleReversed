using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<Monster> monsters = new List<Monster>();
    public Room nextRoom;
    public bool isEndRoom;

    public IEnumerator MakeAttack(Character character)
    {
        foreach (var monster in monsters)
        {
            yield return new WaitForSeconds(1.0f);
            monster.Attack(character);
        }
    }

    public Room GetNextRoom() { return nextRoom; }

    public bool IsEndRoom() { return isEndRoom; }
}
