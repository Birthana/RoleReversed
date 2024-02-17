using UnityEngine;

[CreateAssetMenu(fileName = "EmeraldSlime", menuName = "CardInfo/EmeraldSlime")]
public class EmeraldSlime : MonsterCardInfo
{
    public TemporaryMonster tempMonsterCardInfo;

    public override void Exit(Character self)
    {
        var player = FindObjectOfType<Player>();
        var room = player.gameObject.GetComponentInParent<Room>();
        room.SpawnTemporaryMonster(tempMonsterCardInfo);
    }
}
