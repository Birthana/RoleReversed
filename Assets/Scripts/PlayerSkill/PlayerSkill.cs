using System.Collections;
using UnityEngine;

public class PlayerSkill
{
    private int timer;
    private int currentTimer;
    private SkillInfo info;

    public PlayerSkill(int timer)
    {
        this.timer = timer;
        currentTimer = timer;
    }

    public PlayerSkill(SkillInfo skillInfo)
    {
        timer = skillInfo.timer;
        currentTimer = timer;
        info = skillInfo;
    }

    public int GetTimer() { return currentTimer; }

    public string GetDescription() { return info.description; }

    public Sprite GetSprite() { return info.sprite; }

    public IEnumerator ReduceTimer(int reduceAmount, Room room)
    {
        currentTimer -= reduceAmount;

        if (GetTimer() == 0)
        {
            currentTimer = timer;
            yield return info.Cast(room);
        }

        yield break;
    }
}