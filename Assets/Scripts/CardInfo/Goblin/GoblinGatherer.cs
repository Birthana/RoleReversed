using UnityEngine;

[CreateAssetMenu(fileName = "GoblinGatherer", menuName = "CardInfo/Goblin/GoblinGatherer")]
public class GoblinGatherer : MonsterCardInfo
{
    private int newDamage;
    private int newHealth;

    public override int GetDamage()
    {
        return newDamage;
    }

    public override int GetHealth()
    {
        return newHealth;
    }

    public override void Entrance(Monster self)
    {
        newDamage = damage;
        newHealth = health;
        var room = self.GetCurrentRoom();

        if (room.GetCapacity() > 1)
        {
            self.SpawnEntranceIcon();
            room.ReduceMaxCapacity(1);
            FindObjectOfType<ActionManager>().AddActions(1);
            newDamage = damage + 1;
            newHealth = health + 1;
            self.IncreaseStats(1, 1);
        }
    }
}

