using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "GrowGift", menuName = "RoomateEffect/Grow")]
public class GrowGift : RoommateEffectInfo
{
    public override IEnumerator BattleStart(Room room)
    {
        var randomMonster = room.GetRandomMonster();
        if (randomMonster != null)
        {
            randomMonster.IncreaseStats(2, 2);
        }

        yield return null;
    }
}
