using UnityEngine;

[CreateAssetMenu(fileName = "GreenOrangeSlime", menuName = "CardInfo/GreenOrangeSlime")]
public class GreenOrangeSlime : MonsterCardInfo
{
    public TemporaryMonster tempMonsterCardInfo;

    public override void Exit(Character self)
    {
        var parentRoom = self.transform.parent.GetComponent<Room>();
        var adjacentRooms = new RoomTransform(parentRoom.transform).GetAdjacentRooms();
        foreach (var room in adjacentRooms)
        {
            room.SpawnTemporaryMonster(tempMonsterCardInfo);
        }
    }
}
