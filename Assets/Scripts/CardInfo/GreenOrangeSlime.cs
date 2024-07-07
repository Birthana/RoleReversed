using UnityEngine;

[CreateAssetMenu(fileName = "GreenOrangeSlime", menuName = "CardInfo/GreenOrangeSlime")]
public class GreenOrangeSlime : MonsterCardInfo
{
    public TemporaryMonster tempMonsterCardInfo;

    public override void Exit(Character self)
    {
        SpawnExitIcon(self.transform.position);
        var parentRoom = self.transform.parent.GetComponent<Room>();
        var adjacentRooms = new RoomTransform().GetAdjacentRooms(parentRoom.GetStartPosition());
        foreach (var room in adjacentRooms)
        {
            room.SpawnTemporaryMonsterInDifferentRoom(tempMonsterCardInfo);
        }
    }
}
