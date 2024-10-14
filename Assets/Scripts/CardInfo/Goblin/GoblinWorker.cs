using UnityEngine;

[CreateAssetMenu(fileName = "GoblinWorker", menuName = "CardInfo/Goblin/GoblinWorker")]
public class GoblinWorker : MonsterCardInfo
{
    public override void Entrance(Monster self)
    {
        self.SpawnEntranceIcon();
        self.GetCurrentRoom().IncreaseMaxCapacity(1);
    }
}
