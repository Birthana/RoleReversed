using UnityEngine;

[CreateAssetMenu(fileName = "GreenStatue", menuName = "CardInfo/Statue/GreenStatue")]
public class GreenStatue : MonsterCardInfo
{
    public override void Engage(Monster characterSelf, Card cardSelf)
    {
        var parentRoom = characterSelf.GetCurrentRoom();
        characterSelf.SpawnEngageIcon();
        var pushedMonster = parentRoom.PushRandomRoomMonster(characterSelf);
        if (pushedMonster == null)
        {
            return;
        }

        pushedMonster.IncreaseStats(0, 2);
    }
}
