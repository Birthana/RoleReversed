using UnityEngine;

[CreateAssetMenu(fileName = "GoblinMiner", menuName = "CardInfo/Goblin/GoblinMiner")]
public class GoblinMiner : MonsterCardInfo
{
    public override void Engage(Monster characterSelf, Card cardSelf)
    {
        var room = characterSelf.GetComponentInParent<Room>();
        if(room.HasCapacity())
        {
            characterSelf.SpawnEngageIcon();
            room.ReduceMaxCapacity(1);
            FindObjectOfType<Deck>().DrawCardToHand();
            characterSelf.IncreaseStats(2, 2);
        }
    }
}
