using UnityEngine;

[CreateAssetMenu(fileName = "WhiteStatue", menuName = "CardInfo/Statue/WhiteStatue")]
public class WhiteStatue : MonsterCardInfo
{
    private Monster monsterSelf;

    public override void Global(Monster self)
    {
        monsterSelf = self;
        FindObjectOfType<GlobalEffects>().AddToEntrance(OnEntrancePushSelf);
    }

    public void OnEntrancePushSelf(Monster monster)
    {
        if (monsterSelf == null || monsterSelf.IsDead() || monster == monsterSelf)
        {
            return;
        }

        FindObjectOfType<EffectIcons>().SpawnEntranceIcon(monster.GetCurrentPosition());
        monster.GetCurrentRoom().PushRoomMonster(monster);
    }
}
