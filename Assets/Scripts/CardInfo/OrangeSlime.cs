using UnityEngine;

[CreateAssetMenu(fileName = "OrangeSlime", menuName = "CardInfo/OrangeSlime")]
public class OrangeSlime : MonsterCardInfo
{
    public override void Entrance(Character self)
    {
        SpawnEntranceIcon(self.transform.position);
        var parentRoom = self.transform.parent.GetComponent<Room>();
        var adjacentRooms = new RoomTransform(parentRoom.transform).GetAdjacentRooms();
        foreach(var room in adjacentRooms)
        {
            var monster = room.GetRandomMonster();
            if (monster == null)
            {
                continue;
            }

            monster.IncreaseStats(1, 1);
        }
    }
}
