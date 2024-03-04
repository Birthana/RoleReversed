using UnityEngine;

[CreateAssetMenu(fileName = "OrangeGraySlime", menuName = "CardInfo/OrangeGraySlime")]
public class OrangeGraySlime : MonsterCardInfo
{
    public override void Entrance(Character self)
    {
        var parentRoom = self.transform.parent.GetComponent<Room>();
        var adjacentRooms = new RoomTransform(parentRoom.transform).GetAdjacentRooms();
        var deck = FindObjectOfType<Deck>();
        foreach (var room in adjacentRooms)
        {
            if (room.IsEmpty())
            {
                deck.DrawCardToHand();
            }
        }
    }
}
