using UnityEngine;

[CreateAssetMenu(fileName = "GoblinMiner", menuName = "CardInfo/GoblinMiner")]
public class GoblinMiner : MonsterCardInfo
{
    public override void Engage(Character characterSelf, Card cardSelf)
    {
        var room = characterSelf.GetComponentInParent<Room>();
        if(room.HasCapacity())
        {
            room.ReduceMaxCapacity(1);
            FindObjectOfType<Deck>().DrawCardToHand();
            ((Monster)characterSelf).IncreaseStats(2, 2);
        }
    }
}
