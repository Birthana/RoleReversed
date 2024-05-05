using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    public List<PlayerSkill> skills = new List<PlayerSkill>();
    public SkillDisplay skillPrefab;
    [SerializeField] private List<SkillDisplay> skillDisplays = new List<SkillDisplay>();
    [SerializeField] private Vector3 uiOffset = Vector3.zero;

    public void Add(PlayerSkill playerSkill)
    {
        skills.Add(playerSkill);
    }

    public int GetNumberOfSkills()
    {
        return skills.Count;
    }

    public IEnumerator ReduceSkills(int reduceAmount)
    {
        var player = FindObjectOfType<Player>();
        var currentRoom = player.transform.parent.GetComponent<Room>();
        for (int i = 0; i < skills.Count; i++)
        {
            yield return skills[i].ReduceTimer(reduceAmount, currentRoom);
            DisplaySkills();
        }
    }

    public IEnumerator ShowSkills(int reduceAmount)
    {
        yield return ReduceSkills(reduceAmount);
        yield return new WaitForSeconds(0.25f);
        ClearSkillDisplays();
    }

    public void ToggleDisplay()
    {
        if (skillDisplays.Count == 0)
        {
            DisplaySkills();
            return;
        }

        ClearSkillDisplays();
    }

    public void DisplaySkills()
    {
        ClearSkillDisplays();

        if (skills.Count == 0)
        {
            return;
        }

        foreach (var skill in skills)
        {
            var skillDisplay = Instantiate(skillPrefab, transform);
            skillDisplay.SetSkill(skill);
            skillDisplays.Add(skillDisplay);
        }

        var centerPosition = new CenterPosition(uiOffset, skillDisplays.Count, 25);
        for (int i = 0; i < skillDisplays.Count; i++)
        {
            skillDisplays[i].transform.localPosition = centerPosition.GetVerticalOffsetPositionAt(i);
        }
    }

    public void ClearSkillDisplays()
    {
        foreach (var skillDisplay in skillDisplays)
        {
            DestroyImmediate(skillDisplay.gameObject);
        }

        skillDisplays = new List<SkillDisplay>();
    }
}
