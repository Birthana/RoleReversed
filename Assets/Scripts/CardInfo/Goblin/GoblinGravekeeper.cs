using UnityEngine;

[CreateAssetMenu(fileName = "GoblinGravekeeper", menuName = "CardInfo/Goblin/GoblinGravekeeper")]
public class GoblinGravekeeper : MonsterCardInfo
{
    public override void Exit(Monster self)
    {
        var room = self.GetCurrentRoom();

        if (room.GetCapacity() > 0)
        {
            FindObjectOfType<EffectIcons>().SpawnEntranceIcon(self.GetCurrentPosition());
            room.ReduceMaxCapacity(1);
            FindObjectOfType<PlayerSoulCounter>().IncreaseSouls();
        }
    }
}
