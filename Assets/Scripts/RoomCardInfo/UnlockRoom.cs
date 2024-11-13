using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "UnlockRoom", menuName = "RoomCardInfo/UnlockRoom")]
public class UnlockRoom : RoomCardInfo
{
    public override IEnumerator BuildStart(Room room)
    {
        var monsters = room.monsters;
        room.UnlockMonsters(monsters);
        yield return null;
    }
}
