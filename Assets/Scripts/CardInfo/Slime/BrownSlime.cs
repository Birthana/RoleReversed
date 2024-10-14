using UnityEngine;

[CreateAssetMenu(fileName = "BrownSlime", menuName = "CardInfo/Slime/BrownSlime")]
public class BrownSlime : MonsterCardInfo
{
    public override void Engage(Monster characterSelf, Card cardSelf)
    {
        characterSelf.SpawnEngageIcon();
        var player = FindObjectOfType<Player>();
        var room = player.gameObject.GetComponentInParent<Room>();
        var monsters = room.GetComponentsInChildren<Monster>();
        foreach(var monster in monsters)
        {
            monster.IncreaseStats(1, 1);
        }
    }
}
