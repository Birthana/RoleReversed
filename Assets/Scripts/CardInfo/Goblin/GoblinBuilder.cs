using UnityEngine;

[CreateAssetMenu(fileName = "GoblinBuilder", menuName = "CardInfo/Goblin/GoblinBuilder")]
public class GoblinBuilder : MonsterCardInfo
{
    public override void Entrance(Monster self)
    {
        FindObjectOfType<EffectIcons>().SpawnEntranceIcon(self.GetCurrentPosition());
        self.GetCurrentRoom().IncreaseMaxCapacity(2);
    }
}
