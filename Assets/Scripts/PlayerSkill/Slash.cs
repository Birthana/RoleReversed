using System.Linq;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Slash", menuName = "PlayerSkill/Slash")]
public class Slash : SkillInfo
{
    public Animator animationPrefab;

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

            var animation = Instantiate(animationPrefab);
            animation.transform.position = monster.transform.position;
            monster.TakeDamage(1);
            yield return new WaitForSeconds(0.2f);
            Destroy(animation.gameObject);

            if (monster.IsDead())
            {
                room.Remove(monster);
            }
        }
    }
}
