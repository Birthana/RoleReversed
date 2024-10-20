using UnityEngine;

[CreateAssetMenu(fileName = "GoblinArchitect", menuName = "CardInfo/Goblin/GoblinArchitect")]
public class GoblinArchitect : MonsterCardInfo
{
    public override void Entrance(Monster self)
    {
        var room = self.GetCurrentRoom();
        FindObjectOfType<EffectIcons>().SpawnEntranceIcon(self.GetCurrentPosition());
        var monsters = room.monsters;
        foreach (var monster in monsters)
        {
            monster.Unlock();
        }
    }
}
