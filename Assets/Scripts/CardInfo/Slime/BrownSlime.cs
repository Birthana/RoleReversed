using UnityEngine;

[CreateAssetMenu(fileName = "BrownSlime", menuName = "CardInfo/Slime/BrownSlime")]
public class BrownSlime : MonsterCardInfo
{
    public override void Engage(EffectInput input)
    {
        FindObjectOfType<EffectIcons>().SpawnEngageIcon(input.position);
        var monsters = input.room.monsters;
        foreach(var monster in monsters)
        {
            monster.IncreaseStats(1, 1);
        }
    }
}
