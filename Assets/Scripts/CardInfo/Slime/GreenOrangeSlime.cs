using UnityEngine;

[CreateAssetMenu(fileName = "GreenOrangeSlime", menuName = "CardInfo/Slime/GreenOrangeSlime")]
public class GreenOrangeSlime : MonsterCardInfo
{
    public TemporaryMonster tempMonsterCardInfo;

    public override void Exit(Monster self)
    {
        FindObjectOfType<EffectIcons>().SpawnExitIcon(self.GetCurrentPosition());
        var adjacentRooms = self.GetCurrentRoom().GetAdjacentRooms();
        foreach (var room in adjacentRooms)
        {
            room.SpawnTemporaryMonsterInDifferentRoom(tempMonsterCardInfo);
        }
    }
}
