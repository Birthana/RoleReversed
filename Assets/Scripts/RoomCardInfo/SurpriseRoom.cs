using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "SurpriseRoom", menuName = "RoomCardInfo/SurpriseRoom")]
public class SurpriseRoom : RoomCardInfo
{
    public override IEnumerator BattleStart(Room room)
    {
        SpawnBattleStartIcon(room.transform.position);
        var player = FindObjectOfType<Player>();
        yield return room.MakeRandomAttack(player);
    }
}
