using UnityEngine;

[CreateAssetMenu(fileName = "RedBrownSlime", menuName = "CardInfo/RedBrownSlime")]
public class RedBrownSlime : MonsterCardInfo
{
    public override void Exit(Character self)
    {
        var monsters = self.transform.parent.GetComponentsInChildren<Monster>();
        foreach (var monster in monsters)
        {
            monster.IncreaseStats(1, 1);
        }
    }
}
