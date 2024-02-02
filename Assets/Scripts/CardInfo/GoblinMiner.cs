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
            self.GetComponent<Damage>().IncreaseMaxDamageWithoutReset(2);
            self.GetComponent<Health>().IncreaseMaxHealthWithoutReset(2);
        }
    }
}
