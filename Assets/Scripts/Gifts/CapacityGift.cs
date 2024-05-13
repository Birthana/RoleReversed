using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "CapacityGift", menuName = "RoomateEffect/Capacity")]
public class CapacityGift : RoommateEffectInfo
{
    public override IEnumerator BattleStart(Room room)
    {
        if (room.GetMaxCapacity() < 4)
        {
            room.IncreaseMaxCapacity(1);
        }

        yield return null;
    }
}
