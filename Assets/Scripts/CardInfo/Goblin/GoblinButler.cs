using UnityEngine;

[CreateAssetMenu(fileName = "GoblinButler", menuName = "CardInfo/Goblin/GoblinButler")]
public class GoblinButler : MonsterCardInfo
{
    public RoommateEffectInfo growGift;

    public override void Entrance(Monster self)
    {
        var room = self.GetCurrentRoom();

        if (room.GetCapacity() > 2)
        {
            FindObjectOfType<EffectIcons>().SpawnEntranceIcon(self.GetCurrentPosition());
            room.ReduceMaxCapacity(2);
            room.AddRoommateEffect(growGift, null);
        }
    }
}
