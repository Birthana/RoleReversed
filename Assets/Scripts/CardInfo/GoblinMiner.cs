using UnityEngine;

[CreateAssetMenu(fileName = "GoblinMiner", menuName = "CardInfo/GoblinMiner")]
public class GoblinMiner : MonsterCardInfo
{
    public override void Engage(Character self)
    {
        var room = self.GetComponentInParent<Room>();
        if(room.HasCapacity())
        {
            room.ReduceCapacity(1);
            FindObjectOfType<Deck>().DrawCardToHand();
            ((Monster)self).IncreaseStats(2, 2);
        }
    }
}
