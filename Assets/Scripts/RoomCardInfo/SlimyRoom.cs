using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "SlimyRoom", menuName = "RoomCardInfo/SlimyRoom")]
public class SlimyRoom : RoomCardInfo
{
    public TemporaryMonster tempMonsterCardInfo;

    public override IEnumerator BattleStart(Room room)
    {
        SpawnBattleStartIcon(room.transform.position);
        room.SpawnTemporaryMonster(tempMonsterCardInfo);
        yield return null;
    }
}
