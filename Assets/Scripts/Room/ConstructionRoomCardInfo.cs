using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRoomCard", menuName = "CardInfo/ConstructionRoom")]
public class ConstructionRoomCardInfo : RoomCardInfo
{
    public int timer;
    public RoomCardInfo roomCardInfo;
    private static readonly string FIELD_CONSTRUCTION_ROOM_PREFAB = "Prefabs/FieldConstructionRoom";
    private static readonly string CONSTRUCTION_ROOM_CARD_UI_PREFAB = "Prefabs/ConstructionRoomCardUI";
    

    public int GetTimer() { return timer; }

    public override Room GetFieldRoomPrefab()
    {
        return Resources.Load<Room>(FIELD_CONSTRUCTION_ROOM_PREFAB);
    }

    public override CardUI GetCardUI()
    {
        var cardUI = Resources.Load<ConstructionRoomCardUI>(CONSTRUCTION_ROOM_CARD_UI_PREFAB);
        return cardUI;
    }
}
