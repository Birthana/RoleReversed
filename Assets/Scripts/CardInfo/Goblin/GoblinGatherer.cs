using UnityEngine;

[CreateAssetMenu(fileName = "GoblinGatherer", menuName = "CardInfo/Goblin/GoblinGatherer")]
public class GoblinGatherer : MonsterCardInfo
{
    public override void Entrance(Monster self)
    {
        var room = self.GetCurrentRoom();

        if (room.GetCapacity() > 1)
        {
            FindObjectOfType<EffectIcons>().SpawnEntranceIcon(self.GetCurrentPosition());
            room.ReduceMaxCapacity(1);
            FindObjectOfType<ActionManager>().AddActions(1);
            self.IncreaseStats(1, 1);
        }
    }
}

