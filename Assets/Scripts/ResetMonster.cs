using System;
using System.Collections.Generic;
using UnityEngine;

public class ResetMonster : MonoBehaviour
{
    [SerializeField] private List<Monster> monsters;

    public List<Monster> GetMonsters()
    {
        return monsters;
    }

    public void SetMonsters(List<Monster> monsters)
    {
        this.monsters = monsters;
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
        SetMonsters(new List<Monster>(FindObjectsOfType<Monster>()));
        foreach (var monster in GetMonsters())
        {
            if (monster.isTemporary)
            {
                monster.transform.parent.GetComponent<Room>().Remove(monster);
            }
        }

        SetMonsters(new List<Monster>(FindObjectsOfType<Monster>()));
    }
}
