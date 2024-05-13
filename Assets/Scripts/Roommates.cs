using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roommates
{
    private List<Monster> monsters;
    private List<Monster> requests;

    public Roommates()
    {
        monsters = new List<Monster>();
        requests = new List<Monster>();
    }

    public Roommates(List<Monster> monsters)
    {
        this.monsters = monsters;
        requests = new List<Monster>();
    }

    public List<Monster> Get()
    {
        return monsters;
    }

    public List<Monster> GetRequest()
    {
        return requests;
    }

    public List<Monster> CreateRequest()
    {
        if (monsters.Count < 2 || MonstersAreInTheSameRoom(monsters))
        {
            return new List<Monster>();
        }

        requests = GetTwoUniqueMonsters();
        return GetRequest();
    }

    private List<Monster> GetTwoUniqueMonsters()
    {
        List<Monster> randomMonsters;

        do
        {
            randomMonsters = new List<Monster>();
            randomMonsters.Add(monsters[Random.Range(0, monsters.Count)]);
            randomMonsters.Add(monsters[Random.Range(0, monsters.Count)]);
        } while (MonstersAreUnique(randomMonsters) || MonstersAreInTheSameRoom(randomMonsters));

        return randomMonsters;
    }

    private bool MonstersAreUnique(List<Monster> monsters)
    {
        return monsters[0] == monsters[1];
    }

    public bool MonstersAreInTheSameRoom(List<Monster> monsters)
    {
        var roomPosition = monsters[0].transform.parent.position;
        for (int i = 1; i < monsters.Count; i++)
        {
            if (roomPosition != monsters[i].transform.parent.position)
            {
                return false;
            }
        }

        return true;
    }
}
