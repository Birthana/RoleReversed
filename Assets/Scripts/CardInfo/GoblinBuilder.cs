using UnityEngine;

[CreateAssetMenu(fileName = "GoblinBuilder", menuName = "CardInfo/GoblinBuilder")]
public class GoblinBuilder : MonsterCardInfo
{
    public override void Entrance(Character self)
    {
        SpawnEntranceIcon(self.transform.position);
        var room = self.GetComponentInParent<Room>();
        room.IncreaseMaxCapacity(2);
    }
}
