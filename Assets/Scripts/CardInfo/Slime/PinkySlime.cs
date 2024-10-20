using UnityEngine;

[CreateAssetMenu(fileName = "PinkySlime", menuName = "CardInfo/Slime/PinkySlime")]
public class PinkySlime : MonsterCardInfo
{
    private Monster monsterSelf;
    public TemporaryMonster tempMonsterCardInfo;

    public override void Global(Monster self)
    {
        monsterSelf = self;
        FindObjectOfType<GlobalEffects>().AddToExit(OnExitSpawnSlime);
    }

    public void OnExitSpawnSlime(Monster self)
    {
        if (!self.cardInfo.tags.Contains(Tag.Slime) || self.cardInfo is TemporaryMonster || monsterSelf.IsDead())
        {
            return;
        }

        FindObjectOfType<EffectIcons>().SpawnExitIcon(self.GetCurrentPosition());
        self.GetCurrentRoom().SpawnTemporaryMonster(tempMonsterCardInfo);
    }
}
