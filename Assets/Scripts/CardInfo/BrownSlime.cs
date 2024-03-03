using UnityEngine;

[CreateAssetMenu(fileName = "BrownSlime", menuName = "CardInfo/BrownSlime")]
public class BrownSlime : MonsterCardInfo
{
    public override void Engage(Character self)
    {
        var monsters = self.transform.parent.GetComponentsInChildren<Monster>();
        foreach(var monster in monsters)
        {
            monster.IncreaseStats(1, 1);
        }
    }
}
