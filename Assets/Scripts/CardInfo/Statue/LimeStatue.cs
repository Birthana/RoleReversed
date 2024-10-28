using UnityEngine;

[CreateAssetMenu(fileName = "LimeStatue", menuName = "CardInfo/Statue/LimeStatue")]
public class LimeStatue : MonsterCardInfo
{
    public override void Entrance(Monster self)
    {
        var parentRoom = self.GetCurrentRoom();
        FindObjectOfType<EffectIcons>().SpawnEntranceIcon(self.GetCurrentPosition());
        parentRoom.SpawnRandomCopyNot(self);
    }
}
