using UnityEngine;

[CreateAssetMenu(fileName = "OrangeGraySlime", menuName = "CardInfo/Slime/OrangeGraySlime")]
public class OrangeGraySlime : MonsterCardInfo
{
    private Monster monsterSelf;

    public override void Global(Monster self)
    {
        monsterSelf = self;
        FindObjectOfType<GlobalEffects>().AddToEngage(OnEngage);
    }

    public void OnEngage(EffectInput input)
    {
        if (input.monster.cardInfo is not TemporaryMonster || monsterSelf.IsDead())
        {
            return;
        }

        FindObjectOfType<EffectIcons>().SpawnEngageIcon(input.position);
        var randomMonster = input.room.GetRandomMonsterNot(input.monster);
        if (randomMonster != null)
        {
            randomMonster.TemporaryIncreaseStats(2, 2);
        }
    }
}
