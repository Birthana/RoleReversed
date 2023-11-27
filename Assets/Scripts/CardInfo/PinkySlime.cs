using UnityEngine;

[CreateAssetMenu(fileName = "PinkySlime", menuName = "CardInfo/PinkySlime")]
public class PinkySlime : MonsterCardInfo
{
    public int baseDamage;
    public int baseHealth;

    public override void Entrance()
    {
        int roomCount = FindObjectsOfType<Room>().Length;
        damage = baseDamage + (2 * roomCount);
        health = baseHealth + (2 * roomCount);
    }
}
