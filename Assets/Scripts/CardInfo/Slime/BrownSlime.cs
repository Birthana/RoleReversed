using UnityEngine;

[CreateAssetMenu(fileName = "BrownSlime", menuName = "CardInfo/Slime/BrownSlime")]
public class BrownSlime : MonsterCardInfo
{
    public TemporaryMonster tempMonsterCardInfo;

    public override void Engage(EffectInput input)
    {
        FindObjectOfType<EffectIcons>().SpawnEngageIcon(input.position);
        input.room.SpawnTemporaryMonster(tempMonsterCardInfo);
    }
}
