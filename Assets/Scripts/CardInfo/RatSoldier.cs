using UnityEngine;

[CreateAssetMenu(fileName = "RatSoldier", menuName = "CardInfo/RatSoldier")]
public class RatSoldier : MonsterCardInfo
{
    private Player player;
    private Hand hand;

    private Player GetPlayer()
    {
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }

        return player;
    }

    private Hand GetHand()
    {
        if (hand == null)
        {
            hand = FindObjectOfType<Hand>();
        }

        return hand;
    }

    private void TriggerHandMonsterEngage(MonsterCard randomHandMonster, Monster characterSelf)
    {
        var randomHandMonsterCardInfo = (MonsterCardInfo)randomHandMonster.GetCardInfo();
        randomHandMonsterCardInfo.Engage(characterSelf, randomHandMonster);
    }

    private bool CardIsInHand(Card cardSelf)
    {
        return cardSelf.transform.parent == GetHand().transform;
    }

    private bool RatSoldierIsInHand(Card cardSelf)
    {
        return cardSelf != null && CardIsInHand(cardSelf);
    }

    public override void Engage(Monster characterSelf, Card cardSelf)
    {
        if (GetPlayer().IsDead() || RatSoldierIsInHand(cardSelf))
        {
            return;
        }

        var randomHandMonster = GetHand().RandomMonsterAttacks();
        characterSelf.SpawnEngageIcon();
        if (randomHandMonster == null || GetPlayer().IsDead())
        {
            return;
        }

        TriggerHandMonsterEngage(randomHandMonster, characterSelf);
    }
}
