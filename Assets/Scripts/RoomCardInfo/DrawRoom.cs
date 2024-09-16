using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "DrawRoom", menuName = "RoomCardInfo/DrawRoom")]
public class DrawRoom : RoomCardInfo
{
    public override IEnumerator BuildStart(Room room)
    {
        FindObjectOfType<Deck>().DrawCardToHand();
        yield return null;
    }
}

