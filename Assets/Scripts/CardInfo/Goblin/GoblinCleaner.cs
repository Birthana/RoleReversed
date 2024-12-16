using UnityEngine;

[CreateAssetMenu(fileName = "GoblinCleaner", menuName = "CardInfo/Goblin/GoblinCleaner")]
public class GoblinCleaner : MonsterCardInfo
{
    private Monster monsterSelf;

    public override void Global(Monster self)
    {
        monsterSelf = self;
        FindObjectOfType<GlobalEffects>().AddToRoomEntrance(OnRoomEntranceIncreaseCapacity);
    }

    public void OnRoomEntranceIncreaseCapacity(Room room)
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
        FindObjectOfType<ActionManager>().AddActions(1);
    }
}
