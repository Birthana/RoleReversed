using UnityEngine;

[CreateAssetMenu(fileName = "VioletSlime", menuName = "CardInfo/VioletSlime")]
public class VioletSlime : MonsterCardInfo
{
    public override void Entrance(Character self)
    {
        FindObjectOfType<Player>().ReduceDamage(1);
    }
}
