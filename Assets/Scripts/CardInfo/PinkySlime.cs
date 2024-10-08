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

    public override void Entrance(Monster self)
    {
        self.SpawnEntranceIcon();
        int roomCount = FindObjectsOfType<Room>().Length;
        newDamage = damage + (2 * roomCount);
        newHealth = health + (2 * roomCount);
        self.IncreaseStats(2 * roomCount, 2 * roomCount);
    }
}
