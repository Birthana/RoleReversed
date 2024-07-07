using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "OrangeRoom", menuName = "RoomCardInfo/OrangeRoom")]
public class OrangeRoom : RoomCardInfo
{
    public override IEnumerator BattleStart(Room room)
    {
        SpawnBattleStartIcon(room.transform.position);
        var monster = room.GetRandomMonster();
        monster.Entrance();
        yield return null;
    }
}
