using UnityEngine;

[CreateAssetMenu(fileName = "GreenPurpleSlime", menuName = "CardInfo/GreenPurpleSlime")]
public class GreenPurpleSlime : MonsterCardInfo
{
    public TemporaryMonster tempMonsterCardInfo;

    public override void Entrance(Monster self)
    {
        self.SpawnEntranceIcon();
        self.GetCurrentRoom().SpawnTemporaryMonsterInDifferentRoom(tempMonsterCardInfo);
    }
}
