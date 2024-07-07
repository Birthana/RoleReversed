using UnityEngine;

[CreateAssetMenu(fileName = "GoblinWorker", menuName = "CardInfo/GoblinWorker")]
public class GoblinWorker : MonsterCardInfo
{
    public override void Entrance(Character self)
    {
        SpawnEntranceIcon(self.transform.position);
        var room = self.GetComponentInParent<Room>();
        room.IncreaseMaxCapacity(1);
    }
}
