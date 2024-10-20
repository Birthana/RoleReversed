using UnityEngine;

[CreateAssetMenu(fileName = "DonutSlime", menuName = "CardInfo/Slime/DonutSlime")]
public class DonutSlime : MonsterCardInfo
{
    private Monster monsterSelf;

    public override void Global(Monster self)
    {
        monsterSelf = self;
        FindObjectOfType<GlobalEffects>().AddToExit(OnExitDealDamage);
    }

    public void OnExitDealDamage(Monster self)
    {
        if (self.cardInfo is not TemporaryMonster || monsterSelf.IsDead())
        {
            return;
        }

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
