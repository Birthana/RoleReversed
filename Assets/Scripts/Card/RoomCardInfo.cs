using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "CardInfo/Room")]
public class RoomCardInfo : CardInfo
{
    public int capacity;

    public virtual void BattleStart() { Debug.Log($"Room."); }
}
