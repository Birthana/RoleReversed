using UnityEngine;

[CreateAssetMenu(fileName = "GoblinTailor", menuName = "CardInfo/Goblin/GoblinTailor")]
public class GoblinTailor : MonsterCardInfo
{
    public override void Exit(Monster self)
    {
        var room = self.GetCurrentRoom();
        if (room.GetMaxCapacity() == 1)
        {
            self.SpawnExitIcon();
            room.IncreaseMaxCapacity(1);
            FindObjectOfType<Deck>().DrawCardToHand();
        }
    }
}
