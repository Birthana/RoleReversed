using UnityEngine;

[CreateAssetMenu(fileName = "OrangeGraySlime", menuName = "CardInfo/OrangeGraySlime")]
public class OrangeGraySlime : MonsterCardInfo
{
    public int numberOfEmptyRooms;
    private int emptyRoomCount;

    public override void Entrance(Character self)
    {
        var parentRoom = self.transform.parent.GetComponent<Room>();
        var adjacentRooms = new RoomTransform(parentRoom.transform).GetAdjacentRooms();
        foreach (var room in adjacentRooms)
        {
            if (room.IsEmpty())
            {
                emptyRoomCount++;
            }
        }

        if (emptyRoomCount >= numberOfEmptyRooms)
        {
            SpawnEntranceIcon(self.transform.position);
            var deck = FindObjectOfType<Deck>();
            deck.DrawCardToHand();
        }
    }
}
