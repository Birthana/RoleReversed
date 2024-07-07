using UnityEngine;

[CreateAssetMenu(fileName = "YellowSlime", menuName = "CardInfo/YellowSlime")]
public class YellowSlime : MonsterCardInfo
{
    public TemporaryMonster tempMonsterCardInfo;

    public override void Exit(Character self)
    {
        SpawnExitIcon(self.transform.position);
        var player = FindObjectOfType<Player>();
        var room = player.gameObject.GetComponentInParent<Room>();
        room.SpawnTemporaryMonster(tempMonsterCardInfo);
        room.SpawnTemporaryMonster(tempMonsterCardInfo);
    }
}
