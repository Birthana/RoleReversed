using UnityEngine;

[CreateAssetMenu(fileName = "OrangeGraySlime", menuName = "CardInfo/Slime/OrangeGraySlime")]
public class OrangeGraySlime : MonsterCardInfo
{
    public int numberOfEmptyRooms;
    private int emptyRoomCount;

    public override void Entrance(Monster self)
    {
        var adjacentRooms = self.GetCurrentRoom().GetAdjacentRooms();
        foreach (var adjacentRoom in adjacentRooms)
        {
            if (adjacentRoom.IsEmpty())
            {
                emptyRoomCount++;
            }
        }

        if (emptyRoomCount >= numberOfEmptyRooms)
        {
            self.SpawnEntranceIcon();
            var deck = FindObjectOfType<Deck>();
            deck.DrawCardToHand();
        }
    }
}
