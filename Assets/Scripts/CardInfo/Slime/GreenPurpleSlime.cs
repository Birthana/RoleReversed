using UnityEngine;

[CreateAssetMenu(fileName = "GreenPurpleSlime", menuName = "CardInfo/Slime/GreenPurpleSlime")]
public class GreenPurpleSlime : MonsterCardInfo
{
    public TemporaryMonster tempMonsterCardInfo;

    public override void Entrance(Monster self)
    {
        FindObjectOfType<EffectIcons>().SpawnEntranceIcon(self.GetCurrentPosition());
        self.GetCurrentRoom().SpawnTemporaryMonsterInDifferentRoom(tempMonsterCardInfo);
    }
}
