using UnityEngine;

[CreateAssetMenu(fileName = "RatTorch", menuName = "CardInfo/Rat/RatTorch")]
public class RatTorch : MonsterCardInfo
{
    public CardInfo ratCardInfo;

    public override void Entrance(Monster self)
    {
        if (GetPlayer().IsDead())
        {
            return;
        }

        FindObjectOfType<EffectIcons>().SpawnEntranceIcon(self.GetCurrentPosition());
        var input = new EffectInput(FindObjectOfType<Player>(), self.GetCurrentRoom(), self.GetCurrentPosition(), self);
        var deck = FindObjectOfType<Deck>();
        var hand = FindObjectOfType<Hand>();
        hand.Add(deck.CreateCardWith(ratCardInfo));
        hand.Add(deck.CreateCardWith(ratCardInfo));

        foreach(var card in hand.hand)
        {
            if (card is not MonsterCard)
            {
                continue;
            }

            if (GetPlayer().IsDead())
            {
                FindObjectOfType<GameManager>().ResetPlayer();
                break;
            }

            var monster = (MonsterCard)card;
            monster.MakeHandAttack(input);
            if (GetPlayer().IsDead())
            {
                FindObjectOfType<GameManager>().ResetPlayer();
                break;
            }

            TriggerHandMonsterEngage(monster, input);
        }
    }
}