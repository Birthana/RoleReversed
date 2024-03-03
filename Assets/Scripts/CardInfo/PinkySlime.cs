using UnityEngine;

[CreateAssetMenu(fileName = "PinkySlime", menuName = "CardInfo/PinkySlime")]
public class PinkySlime : MonsterCardInfo
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
        int roomCount = FindObjectsOfType<Room>().Length;
        newDamage = damage + (2 * roomCount);
        newHealth = health + (2 * roomCount);
        ((Monster)self).IncreaseStats(2 * roomCount, 2 * roomCount);
    }
}
