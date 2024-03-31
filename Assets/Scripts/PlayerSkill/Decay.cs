using UnityEngine;

[CreateAssetMenu(fileName = "Decay", menuName = "PlayerSkill/Decay")]
public class Decay : SkillInfo
{
    public override void Cast(Room room)
    {
        var monster = room.GetRandomMonster();
        monster.GetComponent<Health>().IncreaseMaxHealth(-2);
    }
}
