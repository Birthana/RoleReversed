using UnityEngine;

[CreateAssetMenu(fileName = "Slash", menuName = "PlayerSkill/Slash")]
public class Slash : SkillInfo
{
    public override void Cast(Room room)
    {
        var monsters = room.monsters;
        foreach (var monster in monsters)
        {
            monster.TakeDamage(1);
        }
    }
}
