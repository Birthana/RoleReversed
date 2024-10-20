using UnityEngine;

[CreateAssetMenu(fileName = "GoblinWorker", menuName = "CardInfo/Goblin/GoblinWorker")]
public class GoblinWorker : MonsterCardInfo
{
    public override void Entrance(Monster self)
    {
        FindObjectOfType<EffectIcons>().SpawnEntranceIcon(self.GetCurrentPosition());
        self.GetCurrentRoom().IncreaseMaxCapacity(1);
    }
}
