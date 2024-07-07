using UnityEngine;

[CreateAssetMenu(fileName = "GoblinTailor", menuName = "CardInfo/GoblinTailor")]
public class GoblinTailor : MonsterCardInfo
{
    public override void Exit(Character self)
    {
        var room = self.GetComponentInParent<Room>();
        if (room.GetMaxCapacity() == 1)
        {
            SpawnExitIcon(self.transform.position);
            room.IncreaseMaxCapacity(1);
            FindObjectOfType<Deck>().DrawCardToHand();
        }
    }
}
