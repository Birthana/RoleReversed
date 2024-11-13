using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "CardInfo/Room")]
public class RoomCardInfo : CardInfo
{
    public static readonly string FIELD_ROOM_PREFAB = "Prefabs/FieldRoom";
    private static readonly string ROOM_CARD_PREFAB = "Prefabs/RoomCardPrefab";
    private static readonly string ROOM_CARD_UI_PREFAB = "Prefabs/RoomCardUI";
    private static readonly string DROP_ROOM_CARD_UI_PREFAB = "Prefabs/DropRoomCard";
    public int capacity;

    public virtual IEnumerator BattleStart(Room room) { yield return null; }
    public virtual IEnumerator BuildStart(Room room) { yield return null; }

    public virtual Room GetFieldRoomPrefab()
    {
        return Resources.Load<Room>(FIELD_ROOM_PREFAB);
    }

    public override Card GetCardPrefab()
    {
        var cardPrefab = Resources.Load<RoomCard>(ROOM_CARD_PREFAB);
        return cardPrefab;
    }

    public override CardUI GetCardUI()
    {
        var cardUI = Resources.Load<RoomCardUI>(ROOM_CARD_UI_PREFAB);
        return cardUI;
    }

    public override CardUI GetDropCardUI()
    {
        var cardUI = Resources.Load<RoomCardUI>(DROP_ROOM_CARD_UI_PREFAB);
        return cardUI;
    }
}
