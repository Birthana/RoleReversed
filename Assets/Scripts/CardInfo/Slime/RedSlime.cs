using UnityEngine;

[CreateAssetMenu(fileName = "RedSlime", menuName = "CardInfo/Slime/RedSlime")]
public class RedSlime : MonsterCardInfo
{
    public override void Exit(Monster self)
    {
        FindObjectOfType<EffectIcons>().SpawnExitIcon(self.GetCurrentPosition());
        var player = FindObjectOfType<Player>();
        player.TakeDamage(2);
        if (player.IsDead())
        {
            FindObjectOfType<GameManager>().ResetPlayer();
            return;
        }
    }
}
