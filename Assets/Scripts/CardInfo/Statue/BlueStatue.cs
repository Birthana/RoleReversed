using UnityEngine;

[CreateAssetMenu(fileName = "BlueStatue", menuName = "CardInfo/Statue/BlueStatue")]
public class BlueStatue : MonsterCardInfo
{
    public override void Entrance(Monster self)
    {
        var parentRoom = self.GetCurrentRoom();
        var pushedMonster = parentRoom.PushRandomRoomMonster(self);
        if (pushedMonster == null)
        {
            return;
        }

        FindObjectOfType<EffectIcons>().SpawnEntranceIcon(self.GetCurrentPosition());
        new ChangeSortingLayer(pushedMonster.gameObject).SetToDefault();
        FindObjectOfType<EffectIcons>().SpawnPushIcon(pushedMonster.GetCurrentPosition());
        pushedMonster.Lock();
        pushedMonster.TemporaryIncreaseStats(0, 2);
    }
}
