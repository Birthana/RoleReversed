using UnityEngine;

[CreateAssetMenu(fileName = "RatShaman", menuName = "CardInfo/Rat/RatShaman")]
public class RatShaman : MonsterCardInfo
{
    private Monster monsterSelf;

    public override void Global(Monster self)
    {
        monsterSelf = self;
        FindObjectOfType<GlobalEffects>().AddToHandEngage(OnHandEngagePlayCard);
    }

    public void OnHandEngagePlayCard(EffectInput input)
    {
        if (monsterSelf.IsDead())
        {
            return;
        }

        if (input.room.HasCapacity())
        {
            input.card.CastForFreeAt(input.room);
        }
    }
}
