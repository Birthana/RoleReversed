using UnityEngine;

[CreateAssetMenu(fileName = "OrangeStatue", menuName = "CardInfo/Statue/OrangeStatue")]
public class OrangeStatue : MonsterCardInfo
{
    public override void Entrance(Monster self)
    {
        var parentRoom = self.GetCurrentRoom();
        FindObjectOfType<EffectIcons>().SpawnEntranceIcon(self.GetCurrentPosition());
        var rngMonster = parentRoom.GetRandomMonsterNot(self);
        if (rngMonster == null)
        {
            return;
        }

        var adjacentRooms = self.GetCurrentRoom().GetAdjacentRooms();
        foreach(var adjacentRoom in adjacentRooms)
        {
            var monster = adjacentRoom.SpawnCopy(rngMonster.cardInfo);
            monster.SetMaxStats(1, 1);
        }
    }
}
