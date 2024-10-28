using UnityEngine;

[CreateAssetMenu(fileName = "RatSpearman", menuName = "CardInfo/Rat/RatSpearman")]
public class RatSpearman : MonsterCardInfo
{
    public override void Entrance(Monster self)
    {
        if (GetPlayer().IsDead())
        {
            return;
        }

        FindObjectOfType<EffectIcons>().SpawnEntranceIcon(self.GetCurrentPosition());
        GetHand().IncreaseStats();
    }
}
