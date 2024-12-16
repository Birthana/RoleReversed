using UnityEngine;

[CreateAssetMenu(fileName = "IndigoStatue", menuName = "CardInfo/Statue/IndigoStatue")]
public class IndigoStatue : MonsterCardInfo
{
    public override void Engage(EffectInput input)
    {
        if (input.room.HasNoAdjacentRoom())
        {
            return;
        }

        FindObjectOfType<EffectIcons>().SpawnEngageIcon(input.position);
        if (input.monster != null)
        {
            var pushedMonster = input.room.PushRoomMonster(input.monster);
            if (pushedMonster == null)
            {
                return;
            }

            FindObjectOfType<EffectIcons>().SpawnPushIcon(pushedMonster.GetCurrentPosition());
        }

        var monster = input.room.SpawnCopy(this);
        monster.SetMaxStats(1, 1);
    }
}
