using UnityEngine;

[CreateAssetMenu(fileName = "RedBrownSlime", menuName = "CardInfo/RedBrownSlime")]
public class RedBrownSlime : MonsterCardInfo
{
    public override void Exit(Character self)
    {
        var parentRoom = self.transform.parent.GetComponent<Room>();
        var randomMonster = parentRoom.GetRandomMonster();
        if (randomMonster != null)
        {
            SpawnExitIcon(self.transform.position);
            randomMonster.IncreaseStats(1, 1);
        }
    }
}
