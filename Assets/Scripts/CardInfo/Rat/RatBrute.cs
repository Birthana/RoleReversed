using UnityEngine;

[CreateAssetMenu(fileName = "RatBrute", menuName = "CardInfo/Rat/RatBrute")]
public class RatBrute : MonsterCardInfo
{
    public override void Engage(Monster characterSelf, Card cardSelf)
    {
        var player = FindObjectOfType<Player>();
        if (player.IsDead())
        {
            return;
        }

        characterSelf.SpawnEngageIcon();
        player.TakeDamage(player.GetHealth() / 2);
        if (player.IsDead())
        {
            return;
        }
    }
}
