using UnityEngine;

[CreateAssetMenu(fileName = "RatSoldier", menuName = "CardInfo/RatSoldier")]
public class RatSoldier : MonsterCardInfo
{
    public override void Engage(Character characterSelf, Card cardSelf)
    {
        Debug.Log($"{((Monster)characterSelf).cardInfo.cardName} Engages");
        var player = FindObjectOfType<Player>();
        if (player.IsDead())
        {
            return;
        }

        var hand = FindObjectOfType<Hand>();
        MonsterCard monsterCard = (MonsterCard)hand.GetRandomMonsterCard();
        if (monsterCard == null)
        {
            return;
        }

        monsterCard.PlayChosenAnim();
        MonsterCardInfo monsterCardInfo = (MonsterCardInfo)monsterCard.GetCardInfo();
        player.TakeDamage(monsterCardInfo.damage);
        Debug.Log($"Deal {monsterCardInfo.damage} damage");
        if (player.IsDead())
        {
            return;
        }

        var position = monsterCard.transform.parent;
        if (cardSelf != null)
        {
            position = cardSelf.transform.parent;
        }

        if (position == hand.transform)
        {
            return;
        }

        Debug.Log($"Bonus Attack");
        monsterCardInfo.Engage(characterSelf, monsterCard);
    }
}
