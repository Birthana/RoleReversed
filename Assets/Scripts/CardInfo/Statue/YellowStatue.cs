using UnityEngine;

[CreateAssetMenu(fileName = "YellowStatue", menuName = "CardInfo/Statue/YellowStatue")]
public class YellowStatue : MonsterCardInfo
{
    public override void Entrance(Monster self)
    {
        var parentRoom = self.GetCurrentRoom();
        if (parentRoom.HasNoAdjacentRoom())
        {
            return;
        }

        var pulledMonster = parentRoom.PullRandomAdjacentRoomMonster();
        if (pulledMonster == null)
        {
            return;
        }

        FindObjectOfType<EffectIcons>().SpawnEntranceIcon(self.GetCurrentPosition());
        new ChangeSortingLayer(pulledMonster.gameObject).SetToDefault();
        pulledMonster.Lock();
        FindObjectOfType<EffectIcons>().SpawnPullIcon(pulledMonster.GetCurrentPosition());
        pulledMonster.TemporaryIncreaseStats(0, 2);
    }
}
