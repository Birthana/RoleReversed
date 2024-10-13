using UnityEngine;

[CreateAssetMenu(fileName = "VioletSlime", menuName = "CardInfo/VioletSlime")]
public class VioletSlime : MonsterCardInfo
{
    public override void Entrance(Monster self)
    {
        self.SpawnEntranceIcon();
        FindObjectOfType<Player>().TemporaryDecreaseStats(1, 0);
    }
}
