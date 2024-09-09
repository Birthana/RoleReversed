using UnityEngine;

[CreateAssetMenu(fileName = "RedStatue", menuName = "CardInfo/RedStatue")]
public class RedStatue : MonsterCardInfo
{
    public override void Exit(Monster self)
    {
        var parentRoom = self.GetCurrentRoom();
        if (parentRoom.HasNoAdjacentRoom())
        {
            return;
        }

        self.SpawnExitIcon();
        parentRoom.PullRandomAdjacentRoomMonster();
        parentRoom.PullRandomAdjacentRoomMonster();
    }
}
