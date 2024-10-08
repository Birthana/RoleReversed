using UnityEngine;

[CreateAssetMenu(fileName = "GreenOrangeSlime", menuName = "CardInfo/GreenOrangeSlime")]
public class GreenOrangeSlime : MonsterCardInfo
{
    public TemporaryMonster tempMonsterCardInfo;

    public override void Exit(Monster self)
    {
        self.SpawnExitIcon();
        var adjacentRooms = self.GetCurrentRoom().GetAdjacentRooms();
        foreach (var room in adjacentRooms)
        {
            room.SpawnTemporaryMonsterInDifferentRoom(tempMonsterCardInfo);
        }
    }
}
