using UnityEngine;

[CreateAssetMenu(fileName = "OrangeSlime", menuName = "CardInfo/OrangeSlime")]
public class OrangeSlime : MonsterCardInfo
{
    public override void Entrance(Monster self)
    {
        var adjacentRooms = self.GetCurrentRoom().GetAdjacentRooms();

        if (adjacentRooms.Count == 0)
        {
            return;
        }

        self.SpawnEntranceIcon();

        foreach (var adjacentRoom in adjacentRooms)
        {
            var monster = adjacentRoom.GetRandomMonster();
            if (monster == null)
            {
                continue;
            }

            monster.IncreaseStats(1, 1);
        }
    }
}
