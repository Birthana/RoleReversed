using UnityEngine;

[CreateAssetMenu(fileName = "EmeraldSlime", menuName = "CardInfo/Slime/EmeraldSlime")]
public class EmeraldSlime : MonsterCardInfo
{
    public TemporaryMonster tempMonsterCardInfo;

    public override void Exit(Monster self)
    {
        self.SpawnExitIcon();
        self.GetCurrentRoom().SpawnTemporaryMonster(tempMonsterCardInfo);
    }
}