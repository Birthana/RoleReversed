using UnityEngine;

[CreateAssetMenu(fileName = "VioletSlime", menuName = "CardInfo/VioletSlime")]
public class VioletSlime : MonsterCardInfo
{
    public override void Entrance()
    {
        FindObjectOfType<Player>().ReduceDamage(1);
    }
}
