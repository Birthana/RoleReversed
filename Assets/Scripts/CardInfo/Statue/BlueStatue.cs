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

        self.SpawnEntranceIcon();
        new ChangeSortingLayer(pushedMonster.gameObject).SetToDefault();
        pushedMonster.Lock();
        pushedMonster.TemporaryIncreaseStats(2, 0);
    }
}
