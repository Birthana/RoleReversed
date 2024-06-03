using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "RatRoom", menuName = "RoomCardInfo/RatRoom")]
public class RatRoom : RoomCardInfo
{
    public CardInfo ratCardInfo;

    public override IEnumerator BattleStart(Room room)
    {
        var roomCount = room.monsters.Count;

        for(int i = 0; i < roomCount; i++)
        {
            var player = FindObjectOfType<Player>();
            if (player.IsDead())
            {
                yield break;
            }

            var deck = FindObjectOfType<Deck>();
            var card = (MonsterCard)deck.CreateCardWith(ratCardInfo);
            var hand = FindObjectOfType<Hand>();
            if (hand.IsFull())
            {
                yield break;
            }

            hand.Add(card);
            yield return card.PlayChosenAnimation();
            MonsterCardInfo monsterCardInfo = (MonsterCardInfo)card.GetCardInfo();
            player.TakeDamage(monsterCardInfo.damage);
            if (player.IsDead())
            {
                FindObjectOfType<GameManager>().ResetPlayer();
                yield break;
            }
        }
    }
}
