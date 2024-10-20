using UnityEngine;

[CreateAssetMenu(fileName = "RatSoldier", menuName = "CardInfo/Rat/RatSoldier")]
public class RatSoldier : MonsterCardInfo
{
    private bool CardIsInHand(Card cardSelf)
    {
        return cardSelf.transform.parent == GetHand().transform;
    }

    private bool RatSoldierIsInHand(Card cardSelf)
    {
        return cardSelf != null && CardIsInHand(cardSelf);
    }

    public override void Engage(EffectInput input)
    {
        if (GetPlayer().IsDead() || RatSoldierIsInHand(input.card))
        {
            return;
        }

        var randomHandMonster = GetHand().RandomMonsterAttacks(input);
        FindObjectOfType<EffectIcons>().SpawnEngageIcon(input.position);
        if (randomHandMonster == null || GetPlayer().IsDead())
        {
            return;
        }

        TriggerHandMonsterEngage(randomHandMonster, input);
    }
}
