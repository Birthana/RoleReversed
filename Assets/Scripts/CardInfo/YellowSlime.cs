using UnityEngine;

[CreateAssetMenu(fileName = "YellowSlime", menuName = "CardInfo/YellowSlime")]
public class YellowSlime : MonsterCardInfo
{
    public Monster slimePrefab;

    public override void Exit()
    {
        var player = FindObjectOfType<Player>();
        var room = player.gameObject.GetComponentInParent<Room>();
        var slime = Instantiate(slimePrefab, room.transform);
        room.Add(slime);
        slime = Instantiate(slimePrefab, room.transform);
        room.Add(slime);
    }
}
