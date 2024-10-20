using UnityEngine;

[CreateAssetMenu(fileName = "RatBrute", menuName = "CardInfo/Rat/RatBrute")]
public class RatBrute : MonsterCardInfo
{
    public override void Engage(EffectInput input)
    {
        if (input.player.IsDead())
        {
            return;
        }

        FindObjectOfType<EffectIcons>().SpawnEngageIcon(input.position);
        input.player.TakeDamage(input.player.GetHealth() / 2);
        if (input.player.IsDead())
        {
            return;
        }
    }
}
