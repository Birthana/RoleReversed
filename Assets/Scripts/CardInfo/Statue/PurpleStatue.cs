using UnityEngine;

[CreateAssetMenu(fileName = "PurpleStatue", menuName = "CardInfo/Statue/PurpleStatue")]
public class PurpleStatue : MonsterCardInfo
{
    public override void Exit(Monster self)
    {
        var parentRoom = self.GetCurrentRoom();
        if (parentRoom.HasNoAdjacentRoom())
        {
            return;
        }

        FindObjectOfType<EffectIcons>().SpawnExitIcon(self.GetCurrentPosition());
        var pulledMonster = parentRoom.PullRandomAdjacentRoomMonster();
        FindObjectOfType<EffectIcons>().SpawnPullIcon(pulledMonster.GetCurrentPosition());
    }
}

