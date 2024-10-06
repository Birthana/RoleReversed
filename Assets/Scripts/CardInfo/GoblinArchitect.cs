using UnityEngine;

[CreateAssetMenu(fileName = "GoblinArchitect", menuName = "CardInfo/GoblinArchitect")]
public class GoblinArchitect : MonsterCardInfo
{
    public override void Entrance(Monster self)
    {
        var room = self.GetCurrentRoom();
        self.SpawnEntranceIcon();
        var monsters = room.monsters;
        foreach (var monster in monsters)
        {
            monster.Unlock();
        }
    }
}
