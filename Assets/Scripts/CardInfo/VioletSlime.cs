using UnityEngine;

[CreateAssetMenu(fileName = "VioletSlime", menuName = "CardInfo/VioletSlime")]
public class VioletSlime : MonsterCardInfo
{
    public override void Entrance(Character self)
    {
        SpawnEntranceIcon(self.transform.position);
        FindObjectOfType<Player>().ReduceDamage(1);
    }
}
