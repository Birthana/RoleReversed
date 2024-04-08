using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Slash", menuName = "PlayerSkill/Slash")]
public class Slash : SkillInfo
{
    public override void Cast(Room room)
    {
        var monsters = room.monsters;

        if (monsters.Count == 0)
        {
            return;
        }

        foreach (var monster in monsters.ToList())
        {
            if (monster.IsDead())
            {
                continue;
            }
            
            monster.TakeDamage(1);

            if (monster.IsDead())
            {
                room.Remove(monster);
            }
        }
    }
}
