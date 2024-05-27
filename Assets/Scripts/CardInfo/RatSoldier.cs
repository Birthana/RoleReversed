using UnityEngine;

[CreateAssetMenu(fileName = "RatSoldier", menuName = "CardInfo/RatSoldier")]
public class RatSoldier : MonsterCardInfo
{
    public override void Engage(Character self)
    {
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

        MonsterCardInfo monsterCardInfo = (MonsterCardInfo)monsterCard.GetCardInfo();
        player.TakeDamage(monsterCardInfo.damage);
        if (player.IsDead())
        {
            return;
        }

        if (monsterCard.transform.parent == hand.transform)
        {
            return;
        }

        monsterCardInfo.Engage(self);
    }
}
