using UnityEngine;

[CreateAssetMenu(fileName = "GoblinTailor", menuName = "CardInfo/GoblinTailor")]
public class GoblinTailor : MonsterCardInfo
{
    public override void Exit(Character self)
    {
        var room = self.GetComponentInParent<Room>();
        if (room.GetCapacity() == 0)
        {
            room.IncreaseMaxCapacity(1);
        }
    }
}
