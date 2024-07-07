using UnityEngine;

[CreateAssetMenu(fileName = "RedSlime", menuName = "CardInfo/RedSlime")]
public class RedSlime : MonsterCardInfo
{
    public override void Exit(Character self)
    {
        SpawnExitIcon(self.transform.position);
        var player = FindObjectOfType<Player>();
        player.TakeDamage(2);
        if (player.IsDead())
        {
            FindObjectOfType<GameManager>().ResetPlayer();
            return;
        }
    }
}
