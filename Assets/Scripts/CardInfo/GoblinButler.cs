using UnityEngine;

[CreateAssetMenu(fileName = "GoblinButler", menuName = "CardInfo/GoblinButler")]
public class GoblinButler : MonsterCardInfo
{
    public RoommateEffectInfo growGift;

    public override void Entrance(Monster self)
    {
        var room = self.GetCurrentRoom();

        if (room.GetCapacity() > 2)
        {
            self.SpawnEntranceIcon();
            room.ReduceMaxCapacity(2);
            room.AddRoommateEffect(growGift, null);
        }
    }
}
