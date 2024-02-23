using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "CardInfo/Room")]
public class RoomCardInfo : CardInfo
{
    private static readonly string ROOM_CARD_PREFAB = "Prefabs/RoomCardPrefab";
    public int capacity;

    public virtual IEnumerator BattleStart(Room room) { yield return null; }

    public override Card GetCardPrefab()
    {
        var cardPrefab = Resources.Load<RoomCard>(ROOM_CARD_PREFAB);
        return cardPrefab;
    }
}
