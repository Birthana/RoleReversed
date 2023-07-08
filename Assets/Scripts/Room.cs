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

    public void Add(Monster monster)
    {
        monsters.Add(monster);
    }

    public void Remove(Monster monster)
    {
        monsters.Remove(monster);
        Destroy(monster.gameObject);
    }

    public Monster GetRandomMonster()
    {
        var rngIndex = Random.Range(0, monsters.Count);
        return monsters[rngIndex];
    }

    public Room GetNextRoom() { return nextRoom; }

    public bool IsEndRoom() { return isEndRoom; }

    public bool IsEmpty() { return monsters.Count == 0; }
}
