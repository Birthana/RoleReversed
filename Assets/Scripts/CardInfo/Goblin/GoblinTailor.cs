using UnityEngine;

[CreateAssetMenu(fileName = "GoblinTailor", menuName = "CardInfo/Goblin/GoblinTailor")]
public class GoblinTailor : MonsterCardInfo
{
    private Monster monsterSelf;

    public override void Global(Monster self)
    {
        monsterSelf = self;
        FindObjectOfType<GlobalEffects>().AddToEntrance(OnEntranceUnlock);
    }

    public void OnEntranceUnlock(Monster self)
    {
        if (!self.cardInfo.tags.Contains(Tag.Goblin) || self.cardInfo is TemporaryMonster || monsterSelf.IsDead())
        {
            return;
        }

        FindObjectOfType<EffectIcons>().SpawnEntranceIcon(self.GetCurrentPosition());
        var randomMonster = self.GetCurrentRoom().GetRandomMonsterNot(self);
        if (randomMonster != null)
        {
            randomMonster.Unlock();
        }
    }
}
