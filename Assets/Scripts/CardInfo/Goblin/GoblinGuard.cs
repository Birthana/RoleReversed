using UnityEngine;

[CreateAssetMenu(fileName = "GoblinGuard", menuName = "CardInfo/Goblin/GoblinGuard")]
public class GoblinGuard : MonsterCardInfo
{
    public override void Entrance(Monster self)
    {
        var room = self.GetCurrentRoom();
        if (room.HasCapacity())
        {
            if (room.HasNoAdjacentRoom())
            {
                return;
            }

            FindObjectOfType<EffectIcons>().SpawnEntranceIcon(self.GetCurrentPosition());
            room.ReduceMaxCapacity(1);
            room.UnlockAdjacentRooms();
        }
    }
}
