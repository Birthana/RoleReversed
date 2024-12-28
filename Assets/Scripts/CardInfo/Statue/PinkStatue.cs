using UnityEngine;

[CreateAssetMenu(fileName = "PinkStatue", menuName = "CardInfo/Statue/PinkStatue")]
public class PinkStatue : MonsterCardInfo
{
    private Monster monsterSelf;

    public override void Global(Monster self)
    {
        monsterSelf = self;
        FindObjectOfType<GlobalEffects>().AddToDraftExit(OnDraftExitSpawnCopy);
    }

    public void OnDraftExitSpawnCopy(Card card)
    {
        var cardInfo = card.GetCardInfo();
        if (cardInfo is RoomCardInfo || cardInfo is ConstructionRoomCardInfo || monsterSelf == null || monsterSelf.IsDead())
        {
            return;
        }

        var monsterCardInfo = (MonsterCardInfo)cardInfo;
        var monster = monsterSelf.GetCurrentRoom().SpawnCopy(monsterCardInfo);
        monster.SetMaxStats(1, 1);
    }
}
