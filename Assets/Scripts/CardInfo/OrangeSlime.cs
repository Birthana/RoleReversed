using UnityEngine;

[CreateAssetMenu(fileName = "OrangeSlime", menuName = "CardInfo/OrangeSlime")]
public class OrangeSlime : MonsterCardInfo
{
    public override void Entrance(Character self)
    {
        var parentRoom = self.transform.parent.GetComponent<Room>();
        var adjacentRooms = new RoomTransform(parentRoom.transform).GetAdjacentRooms();
        foreach(var room in adjacentRooms)
        {
            var monster = room.GetRandomMonster();
            if (monster == null)
            {
                continue;
            }

            monster.gameObject.GetComponent<Damage>().IncreaseMaxDamageWithoutReset(1);
            monster.gameObject.GetComponent<Health>().IncreaseMaxHealthWithoutReset(1);
        }
    }
}
