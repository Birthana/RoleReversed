using UnityEngine;

[CreateAssetMenu(fileName = "RatWarrior", menuName = "CardInfo/Rat/RatWarrior")]
public class RatWarrior : MonsterCardInfo
{
    public override void Entrance(Monster self)
    {
        if (GetPlayer().IsDead())
        {
            return;
        }

        FindObjectOfType<EffectIcons>().SpawnEntranceIcon(self.GetCurrentPosition());
        var input = new EffectInput(FindObjectOfType<Player>(), self.GetCurrentRoom(), self.GetCurrentPosition(), self);
        var drawnCard = FindObjectOfType<Deck>().DrawCardToHand();
        if (drawnCard == null || !(drawnCard.GetCardInfo() is MonsterCardInfo))
        {
            return;
        }

        var monsterCard = (MonsterCard)drawnCard;
        monsterCard.MakeHandAttack(input);

        if (GetPlayer().IsDead())
        {
            FindObjectOfType<GameManager>().ResetPlayer();
            return;
        }

        TriggerHandMonsterEngage(monsterCard, input);
    }
}