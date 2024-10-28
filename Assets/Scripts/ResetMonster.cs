using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResetMonster : MonoBehaviour
{
    [SerializeField] private List<Monster> monsters;

    public List<Monster> GetMonsters()
    {
        return monsters;
    }

    public void Add(Monster monster) { monsters.Add(monster); }

    public void SetMonstersLocked()
    {
        foreach (var monster in monsters)
        {
            monster.Lock();
        }
    }

    public void ResetFieldMonsters()
    {
        ResetAllMonsterStats();
        DestroyAllTempMonsters();
    }

    private void ResetAllMonsterStats()
    {
        foreach (var monster in GetMonsters())
        {
            monster.gameObject.SetActive(true);
            monster.ResetStats();
        }
    }

    public void DestroyAllTempMonsters()
    {
        foreach (var monster in GetMonsters().ToList())
        {
            if (monster.isTemporary)
            {
                monster.transform.parent.GetComponent<Room>().RemoveTemporary(monster);
                monsters.Remove(monster);
            }
        }
    }

    public int GetNumberOfTempSlimes()
    {
        int count = 0;

        foreach (var monster in GetMonsters().ToList())
        {
            if (!monster.isTemporary)
            {
                continue;
            }

            count++;
        }

        return count;
    }
}
