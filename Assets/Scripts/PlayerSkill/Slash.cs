using System.Linq;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Slash", menuName = "PlayerSkill/Slash")]
public class Slash : SkillInfo
{
    public override IEnumerator Cast(Room room)
    {
        var monsters = room.monsters;

        if (monsters.Count == 0)
        {
            yield break;
        }

        foreach (var monster in monsters.ToList())
        {
            if (monster.IsDead())
            {
                continue;
            }
            
            monster.TakeDamage(1);
            yield return new WaitForSeconds(0.1f);

            if (monster.IsDead())
            {
                room.Remove(monster);
            }
        }
    }
}
