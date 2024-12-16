using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "GhostRoom", menuName = "RoomCardInfo/GhostRoom")]
public class GhostRoom : RoomCardInfo
{
    public override IEnumerator BattleStart(Room room)
    {
        SpawnBattleStartIcon(room.transform.position);
        foreach (var monster in room.monsters)
        {
            monster.BecomeTemp();
        }

        var player = FindObjectOfType<Player>();
        yield return room.MakeAttacks(player);
        yield return room.MakeAttacks(player);
    }
}
