using UnityEngine;

[CreateAssetMenu(fileName = "EmeraldSlime", menuName = "CardInfo/Slime/EmeraldSlime")]
public class EmeraldSlime : MonsterCardInfo
{
    public TemporaryMonster tempMonsterCardInfo;

    public override void Exit(Monster self)
    {
        FindObjectOfType<EffectIcons>().SpawnExitIcon(self.GetCurrentPosition());
        self.GetCurrentRoom().SpawnTemporaryMonster(tempMonsterCardInfo);
    }
}
