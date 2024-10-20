using UnityEngine;

[CreateAssetMenu(fileName = "RatArcher", menuName = "CardInfo/Rat/RatArcher")]
public class RatArcher : MonsterCardInfo
{
    public override void Entrance(Monster self)
    {
        if (GetPlayer().IsDead())
        {
            return;
        }

        FindObjectOfType<EffectIcons>().SpawnEntranceIcon(self.GetCurrentPosition());
        var input = new EffectInput(GetPlayer(), self.GetCurrentRoom(), self.GetCurrentPosition(), self);
        var randomHandMonster = GetHand().RandomMonsterAttacks(input);
        if (randomHandMonster == null)
        {
            return;
        }

        if (GetPlayer().IsDead())
        {
            Discard(randomHandMonster);
            FindObjectOfType<Deck>().DrawCardToHand();
            FindObjectOfType<GameManager>().ResetPlayer();
            return;
        }

        TriggerHandMonsterEngage(randomHandMonster, input);
        Discard(randomHandMonster);
        FindObjectOfType<Deck>().DrawCardToHand();
    }

    private void Discard(MonsterCard monsterCard)
    {
        GetHand().Remove(monsterCard);
        FindObjectOfType<Drop>().Add(monsterCard.GetCardInfo());
        DestroyImmediate(monsterCard.gameObject);
    }
}
