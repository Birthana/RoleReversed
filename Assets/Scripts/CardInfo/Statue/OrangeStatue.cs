using UnityEngine;

[CreateAssetMenu(fileName = "OrangeStatue", menuName = "CardInfo/Statue/OrangeStatue")]
public class OrangeStatue : MonsterCardInfo
{
    private Monster monsterSelf;

    public override void Global(Monster self)
    {
        monsterSelf = self;
        FindObjectOfType<GlobalEffects>().AddToEntrance(OnEntranceGainHealth);
    }

    public void OnEntranceGainHealth(Monster self)
    {
        if (monsterSelf.IsDead() || (self.GetCurrentRoom() != monsterSelf.GetCurrentRoom()))
        {
            return;
        }

        FindObjectOfType<EffectIcons>().SpawnEntranceIcon(self.GetCurrentPosition());
        var parentRoom = monsterSelf.GetCurrentRoom();
        var monsters = parentRoom.GetComponentsInChildren<Monster>();
        foreach (var monster in monsters)
        {
            monster.TemporaryIncreaseStats(0, 2);
        }
    }
}
