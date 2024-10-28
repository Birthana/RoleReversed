using UnityEngine;

[CreateAssetMenu(fileName = "GoblinTailor", menuName = "CardInfo/Goblin/GoblinTailor")]
public class GoblinTailor : MonsterCardInfo
{
    private Monster monsterSelf;

    public override void Global(Monster self)
    {
        monsterSelf = self;
        FindObjectOfType<GlobalEffects>().AddToEntrance(OnEntranceIncreaseStats);
    }

    public void OnEntranceIncreaseStats(Monster self)
    {
        if (ShouldNotCast(self.cardInfo))
        {
            return;
        }

        FindObjectOfType<EffectIcons>().SpawnEntranceIcon(self.GetCurrentPosition());
        var monsters = self.GetCurrentRoom().monsters;
        foreach (var monster in monsters)
        {
            monster.TemporaryIncreaseStats(2, 2);
        }
    }

    private bool ShouldNotCast(MonsterCardInfo cardInfo)
    {
        return !cardInfo.IsGoblin() ||
                cardInfo is TemporaryMonster ||
                monsterSelf.IsDead();
    }
}
