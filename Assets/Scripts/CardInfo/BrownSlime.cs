using UnityEngine;

[CreateAssetMenu(fileName = "BrownSlime", menuName = "CardInfo/BrownSlime")]
public class BrownSlime : MonsterCardInfo
{
    public override void Engage(Character characterSelf, Card cardSelf)
    {
        SpawnEngageIcon(characterSelf.transform.position);
        var monsters = characterSelf.transform.parent.GetComponentsInChildren<Monster>();
        foreach(var monster in monsters)
        {
            monster.IncreaseStats(1, 1);
        }
    }
}
