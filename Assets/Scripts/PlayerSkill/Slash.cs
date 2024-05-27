using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Slash", menuName = "PlayerSkill/Slash")]
public class Slash : SkillInfo
{
    public Animator animationPrefab;
    private List<Animator> animations = new List<Animator>();

    public override IEnumerator Cast(Room room)
    {
        animations = new List<Animator>();
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

            animations.Add(SpawnAnimation(monster.transform.position));
        }

        yield return new WaitForSeconds(0.15f);

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

        foreach (var animation in animations)
        {
            Destroy(animation.gameObject);
        }
    }

    private Animator SpawnAnimation(Vector3 position)
    {
        var animation = Instantiate(animationPrefab);
        animation.transform.position = position;
        return animation;
    }
}
