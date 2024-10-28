using UnityEngine;

[CreateAssetMenu(fileName = "RatDancer", menuName = "CardInfo/Rat/RatDancer")]
public class RatDancer : MonsterCardInfo
{
    public override void Engage(EffectInput input)
    {
        FindObjectOfType<EffectIcons>().SpawnEngageIcon(input.position);
        var handSize = FindObjectOfType<Hand>().GetSize();
        input.TemporaryIncreaseMonsterStats(handSize, handSize);
    }
}
