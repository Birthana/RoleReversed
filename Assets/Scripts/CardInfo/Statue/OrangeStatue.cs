using UnityEngine;

[CreateAssetMenu(fileName = "OrangeStatue", menuName = "CardInfo/Statue/OrangeStatue")]
public class OrangeStatue : MonsterCardInfo
{
    private Monster monsterSelf;

    public override void Global(Monster self)
    {
        monsterSelf = self;
        FindObjectOfType<GlobalEffects>().AddToExit(OnExitCopy);
    }

    public void OnExitCopy(Monster self)
    {
        if (monsterSelf.IsDead() || !self.cardInfo.IsSlime() || self.isTemporary)
        {
            return;
        }

        var player = FindObjectOfType<Player>();
        if (player.IsDead())
        {
            return;
        }

        FindObjectOfType<EffectIcons>().SpawnEngageIcon(self.GetCurrentPosition());
        self.GetCurrentRoom().SpawnRandomCopyNot(self);
    }
}
