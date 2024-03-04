using UnityEngine;

[CreateAssetMenu(fileName = "GoblinBuilder", menuName = "CardInfo/GoblinBuilder")]
public class GoblinBuilder : MonsterCardInfo
{
    public override void Entrance(Character self)
    {
        var room = self.GetComponentInParent<Room>();
        room.IncreaseMaxCapacity(2);
    }
}
