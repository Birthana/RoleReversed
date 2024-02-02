using UnityEngine;

[CreateAssetMenu(fileName = "GoblinWorker", menuName = "CardInfo/GoblinWorker")]
public class GoblinWorker : MonsterCardInfo
{
    public override void Entrance(Character self)
    {
        var room = self.GetComponentInParent<Room>();
        if (room.CurrentCapacityIsMaxCapacity())
        {
            room.IncreaseCapacity(1);
        }
    }
}
