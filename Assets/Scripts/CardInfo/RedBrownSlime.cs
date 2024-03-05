using UnityEngine;

[CreateAssetMenu(fileName = "RedBrownSlime", menuName = "CardInfo/RedBrownSlime")]
public class RedBrownSlime : MonsterCardInfo
{
    public override void Exit(Character self)
    {
        var parentRoom = self.transform.parent.GetComponent<Room>();
        parentRoom.GetRandomMonster().IncreaseStats(1, 1);
    }
}
