using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "DeadEndRoom", menuName = "RoomCardInfo/DeadEndRoom")]
public class DeadEndRoom : RoomCardInfo
{
    public override IEnumerator BattleStart(Room room)
    {
        if (room.GetAdjacentRooms().Count > 1)
        {
            yield return null;
        }

        SpawnBattleStartIcon(room.transform.position);
        var randomMonster = room.GetRandomMonster();
        if (randomMonster != null)
        {
            randomMonster.IncreaseStats(1, 1);
        }

        yield return null;
    }
}
