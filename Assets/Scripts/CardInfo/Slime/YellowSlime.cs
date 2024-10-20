using UnityEngine;

[CreateAssetMenu(fileName = "YellowSlime", menuName = "CardInfo/Slime/YellowSlime")]
public class YellowSlime : MonsterCardInfo
{
    public TemporaryMonster tempMonsterCardInfo;

    public override void Exit(Monster self)
    {
        FindObjectOfType<EffectIcons>().SpawnExitIcon(self.GetCurrentPosition());
        var room = self.GetCurrentRoom();
        room.SpawnTemporaryMonster(tempMonsterCardInfo);
        room.SpawnTemporaryMonster(tempMonsterCardInfo);
    }
}
