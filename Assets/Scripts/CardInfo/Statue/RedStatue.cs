using UnityEngine;

[CreateAssetMenu(fileName = "RedStatue", menuName = "CardInfo/Statue/RedStatue")]
public class RedStatue : MonsterCardInfo
{
    public override void Exit(Monster self)
    {
        var parentRoom = self.GetCurrentRoom();
        if (parentRoom.HasNoAdjacentRoom())
        {
            return;
        }

        FindObjectOfType<EffectIcons>().SpawnExitIcon(self.GetCurrentPosition());
        PullRoomMonster(parentRoom);
        PullRoomMonster(parentRoom);
    }

    private void PullRoomMonster(Room room)
    {
        var pulledMonster = room.PullRandomAdjacentRoomMonster();
        FindObjectOfType<EffectIcons>().SpawnPullIcon(pulledMonster.GetCurrentPosition());
    }
}
