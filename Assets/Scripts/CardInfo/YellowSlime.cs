using UnityEngine;

[CreateAssetMenu(fileName = "YellowSlime", menuName = "CardInfo/YellowSlime")]
public class YellowSlime : MonsterCardInfo
{
    public TemporaryMonster tempMonsterCardInfo;

    public override void Exit(Monster self)
    {
        self.SpawnExitIcon();
        var room = self.GetCurrentRoom();
        room.SpawnTemporaryMonster(tempMonsterCardInfo);
        room.SpawnTemporaryMonster(tempMonsterCardInfo);
    }
}
