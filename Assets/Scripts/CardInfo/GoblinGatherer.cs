using UnityEngine;

[CreateAssetMenu(fileName = "GoblinGatherer", menuName = "CardInfo/GoblinGatherer")]
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

    public override void Entrance(Character self)
    {
        newDamage = damage;
        newHealth = health;

        var room = self.GetComponentInParent<Room>();
        if (room.GetCapacity() > 1)
        {
            room.ReduceCapacity(1);
            FindObjectOfType<ActionManager>().AddActions(1);
            newDamage = damage + 1;
            newHealth = health + 1;
            ((Monster)self).IncreaseStats(1, 1);
        }
    }
}

