using UnityEngine;

[CreateAssetMenu(fileName = "GoblinCarrier", menuName = "CardInfo/Goblin/GoblinCarrier")]
public class GoblinCarrier : MonsterCardInfo
{
    private Monster monsterSelf;

    public override void Global(Monster self)
    {
        monsterSelf = self;
        FindObjectOfType<GlobalEffects>().AddToRoomEntrance(OnRoomEntranceIncreaseStats);
    }

    public void OnRoomEntranceIncreaseStats(Room room)
    {
        if (monsterSelf == null)
        {
            return;
        }

        if (!monsterSelf.GetCurrentRoom().IsAdjacentTo(room))
        {
            return;
        }

        FindObjectOfType<EffectIcons>().SpawnEntranceIcon(room.GetStartPosition());
        monsterSelf.IncreaseStats(2, 2);
    }
}

