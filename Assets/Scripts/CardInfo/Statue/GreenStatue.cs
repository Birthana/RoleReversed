using UnityEngine;

[CreateAssetMenu(fileName = "GreenStatue", menuName = "CardInfo/Statue/GreenStatue")]
public class GreenStatue : MonsterCardInfo
{
    public override void Engage(EffectInput input)
    {
        FindObjectOfType<EffectIcons>().SpawnEngageIcon(input.position);
        var pushedMonster = input.room.PushRandomRoomMonster(input.monster);
        if (pushedMonster == null)
        {
            return;
        }

        FindObjectOfType<EffectIcons>().SpawnPushIcon(pushedMonster.GetCurrentPosition());
        pushedMonster.TemporaryIncreaseStats(0, 2);
    }
}
