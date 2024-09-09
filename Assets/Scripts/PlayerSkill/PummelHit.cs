using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "PummelHit", menuName = "PlayerSkill/PummelHit")]
public class PummelHit : SkillInfo
{
    public override IEnumerator Cast(Room room)
    {
        var monsters = room.monsters;
        if (monsters.Count == 0)
        {
            yield break;
        }

        room.PushRandomRoomMonster(null);
        yield return new WaitForSeconds(0.15f);
    }
}
