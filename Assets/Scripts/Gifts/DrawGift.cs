using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "DrawGift", menuName = "RoomateEffect/Draw")]
public class DrawGift : RoommateEffectInfo
{
    public override IEnumerator BattleStart(Room room)
    {
        FindObjectOfType<Deck>().DrawCardToHand();
        yield return null;
    }
}
