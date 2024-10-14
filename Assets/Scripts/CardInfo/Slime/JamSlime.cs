using UnityEngine;

[CreateAssetMenu(fileName = "JamSlime", menuName = "CardInfo/Slime/JamSlime")]
public class JamSlime : MonsterCardInfo
{
    public override void Entrance(Monster self)
    {
        self.SpawnEntranceIcon();
        var player = FindObjectOfType<Player>();
        player.TakeDamage(1);
        if (player.IsDead())
        {
            FindObjectOfType<GameManager>().ResetPlayer();
            return;
        }
    }
}
