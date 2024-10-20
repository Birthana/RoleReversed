using UnityEngine;

[CreateAssetMenu(fileName = "RatBannerman", menuName = "CardInfo/Rat/RatBannerman")]
public class RatBannerman : MonsterCardInfo
{
    public override void Entrance(Monster self)
    {
        if (GetPlayer().IsDead())
        {
            return;
        }

        FindObjectOfType<EffectIcons>().SpawnEntranceIcon(self.GetCurrentPosition());
        var input = new EffectInput(FindObjectOfType<Player>(), self.GetCurrentRoom(), self.GetCurrentPosition(), self);
        var randomHandMonster = GetHand().RandomMonsterAttacks(input);
        if (randomHandMonster == null)
        {
            return;
        }

        randomHandMonster.ReduceCost(1);

        if (GetPlayer().IsDead())
        {
            FindObjectOfType<GameManager>().ResetPlayer();
            return;
        }

        TriggerHandMonsterEngage(randomHandMonster, input);
    }
}
