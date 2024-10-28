using UnityEngine;

[CreateAssetMenu(fileName = "GoblinButler", menuName = "CardInfo/Goblin/GoblinButler")]
public class GoblinButler : MonsterCardInfo
{
    public override void Engage(EffectInput input)
    {
        FindObjectOfType<EffectIcons>().SpawnEngageIcon(input.position);
        var count = input.room.monsters.Count;
        input.TemporaryIncreaseMonsterStats(count, count);
    }
}
