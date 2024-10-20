using UnityEngine;

[CreateAssetMenu(fileName = "GoblinMiner", menuName = "CardInfo/Goblin/GoblinMiner")]
public class GoblinMiner : MonsterCardInfo
{
    public override void Engage(EffectInput input)
    {
        if(input.room.HasCapacity())
        {
            FindObjectOfType<EffectIcons>().SpawnEngageIcon(input.position);
            input.room.ReduceMaxCapacity(1);
            FindObjectOfType<Deck>().DrawCardToHand();
            input.IncreaseMonsterStats(2, 2);
        }
    }
}
