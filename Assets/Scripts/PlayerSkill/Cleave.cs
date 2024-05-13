using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Cleave", menuName = "PlayerSkill/Cleave")]
public class Cleave : SkillInfo
{
    public override IEnumerator Cast(Room room)
    {
        if (room.IsEmpty())
        {
            yield break;
        }

        var monster = room.GetRandomMonster();
        monster.TakeDamage(3);
        yield return new WaitForSeconds(0.1f);

        if (monster.IsDead())
        {
            room.Remove(monster);
        }
    }
}
