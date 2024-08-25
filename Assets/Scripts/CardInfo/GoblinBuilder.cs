using UnityEngine;

[CreateAssetMenu(fileName = "GoblinBuilder", menuName = "CardInfo/GoblinBuilder")]
public class GoblinBuilder : MonsterCardInfo
{
    public override void Entrance(Monster self)
    {
        self.SpawnEntranceIcon();
        self.GetCurrentRoom().IncreaseMaxCapacity(2);
    }
}
