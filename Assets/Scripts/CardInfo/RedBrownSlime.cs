using UnityEngine;

[CreateAssetMenu(fileName = "RedBrownSlime", menuName = "CardInfo/RedBrownSlime")]
public class RedBrownSlime : MonsterCardInfo
{
    public override void Exit(Character self)
    {
        var monsters = self.transform.parent.GetComponentsInChildren<Monster>();
        foreach (var monster in monsters)
        {
            monster.gameObject.GetComponent<Damage>().IncreaseMaxDamageWithoutReset(1);
            monster.gameObject.GetComponent<Health>().IncreaseMaxHealthWithoutReset(1);
        }
    }
}
