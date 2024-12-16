using UnityEngine;

[CreateAssetMenu(fileName = "GoblinTailor", menuName = "CardInfo/Goblin/GoblinTailor")]
public class GoblinTailor : MonsterCardInfo
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
        var monsters = FindObjectsOfType<Monster>();
        foreach (var monster in monsters)
        {
            monster.TemporaryIncreaseStats(5, 5);
        }
    }
}
