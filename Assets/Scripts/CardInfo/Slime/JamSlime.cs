using UnityEngine;

[CreateAssetMenu(fileName = "JamSlime", menuName = "CardInfo/Slime/JamSlime")]
public class JamSlime : MonsterCardInfo
{
    public override void Engage(EffectInput input)
    {
        FindObjectOfType<EffectIcons>().SpawnEngageIcon(input.position);
        var numberOfTempSlimes = FindObjectOfType<GameManager>().GetNumberOfTempSlimes();
        input.TemporaryIncreaseMonsterStats(numberOfTempSlimes, numberOfTempSlimes);
    }
}
