using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    public List<PlayerSkill> skills = new List<PlayerSkill>();

    public void Add(PlayerSkill playerSkill)
    {
        skills.Add(playerSkill);
    }

    public int GetNumberOfSkills()
    {
        return skills.Count;
    }

    public void ReduceSkills(int reduceAmount)
    {
        var player = FindObjectOfType<Player>();
        for (int i = 0; i < skills.Count; i++)
        {
            skills[i].ReduceTimer(reduceAmount, player.transform.parent.GetComponent<Room>());
        }
    }
}
