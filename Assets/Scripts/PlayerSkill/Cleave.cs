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
        var animation = Instantiate(animationPrefab);
        animation.transform.position = monster.transform.position;
        monster.TakeDamage(3);
        yield return new WaitForSeconds(0.2f);
        Destroy(animation.gameObject);

        if (monster.IsDead())
        {
            room.Remove(monster);
        }
    }
}
