using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    public List<PlayerSkill> skills = new List<PlayerSkill>();
    public SkillDisplay skillPrefab;
    [SerializeField] private List<SkillDisplay> skillDisplays = new List<SkillDisplay>();

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
            DisplaySkills();
        }
    }

    private void DisplaySkills()
    {
        ClearSkillDisplays();
        foreach (var skill in skills)
        {
            var skillDisplay = Instantiate(skillPrefab, transform);
            skillDisplay.SetSkill(skill);
            skillDisplays.Add(skillDisplay);
        }

        var centerPosition = new CenterPosition(Vector3.zero, skillDisplays.Count, 25);
        for (int i = 0; i < skillDisplays.Count; i++)
        {
            skillDisplays[i].transform.localPosition = centerPosition.GetVerticalOffsetPositionAt(i);
        }
    }

    private void ClearSkillDisplays()
    {
        foreach (var skillDisplay in skillDisplays)
        {
            DestroyImmediate(skillDisplay.gameObject);
        }

        skillDisplays = new List<SkillDisplay>();
    }
}
