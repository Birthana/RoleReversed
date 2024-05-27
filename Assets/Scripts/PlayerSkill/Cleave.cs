using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Cleave", menuName = "PlayerSkill/Cleave")]
public class Cleave : SkillInfo
{
    public Animator animationPrefab;

    public override IEnumerator Cast(Room room)
    {
        if (room.IsEmpty())
        {
            yield break;
        }

        var monster = room.GetRandomMonster();
        var animation = SpawnAnimation(monster.transform.position);
        monster.TakeDamage(3);
        yield return new WaitForSeconds(0.2f);

        if (monster.IsDead())
        {
            room.Remove(monster);
        }

        Destroy(animation.gameObject);
    }

    private Animator SpawnAnimation(Vector3 position)
    {
        var animation = Instantiate(animationPrefab);
        animation.transform.position = position;
        return animation;
    }
}
