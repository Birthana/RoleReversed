using UnityEngine;

[CreateAssetMenu(fileName = "GoblinWorker", menuName = "CardInfo/GoblinWorker")]
public class GoblinWorker : MonsterCardInfo
{
    public override void Entrance(Monster self)
    {
        self.SpawnEntranceIcon();
        self.GetCurrentRoom().IncreaseMaxCapacity(1);
    }
}
