using UnityEngine;

[CreateAssetMenu(fileName = "DonutSlime", menuName = "CardInfo/DonutSlime")]
public class DonutSlime : MonsterCardInfo
{
    public override void Global(Monster self)
    {
        FindObjectOfType<GlobalEffects>().AddToExit(OnExitDealDamage);
    }

    public void OnExitDealDamage(Monster self)
    {
        if (self.cardInfo is not TemporaryMonster)
        {
            return;
        }

        self.SpawnExitIcon();
        var player = FindObjectOfType<Player>();
        player.TakeDamage(2);
        if (player.IsDead())
        {
            FindObjectOfType<GameManager>().ResetPlayer();
            return;
        }
    }
}
