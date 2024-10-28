using UnityEngine;

[CreateAssetMenu(fileName = "GreenStatue", menuName = "CardInfo/Statue/GreenStatue")]
public class GreenStatue : MonsterCardInfo
{
    public override void Engage(EffectInput input)
    {
        FindObjectOfType<EffectIcons>().SpawnEngageIcon(input.position);
        var rooms = FindObjectsOfType<Room>();
        var count = 0;
        foreach(var room in rooms)
        {
            if (room.IsEmpty())
            {
                continue;
            }

            count++;
        }

        input.TemporaryIncreaseMonsterStats(count, count);
    }
}
