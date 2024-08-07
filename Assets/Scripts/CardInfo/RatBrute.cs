using UnityEngine;

[CreateAssetMenu(fileName = "RatBrute", menuName = "CardInfo/RatBrute")]
public class RatBrute : MonsterCardInfo
{
    public override void Engage(Character characterSelf, Card cardSelf)
    {
        SpawnEngageIcon(characterSelf.transform.position);
        var player = FindObjectOfType<Player>();
        player.TakeDamage(player.GetHealth() / 2);
        if (player.IsDead())
        {
            return;
        }
    }
}
