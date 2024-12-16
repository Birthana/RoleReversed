using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "RallyRoom", menuName = "RoomCardInfo/RallyRoom")]
public class RallyRoom : RoomCardInfo
{
    public override IEnumerator BattleStart(Room room)
    {
        foreach (var monster in room.monsters)
        {
            monster.IncreaseStats(1, 1);
        }

        yield return null;
    }
}
