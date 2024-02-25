using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRoomCard", menuName = "CardInfo/ConstructionRoom")]
public class ConstructionRoomCardInfo : RoomCardInfo
{
    public int timer;
    public RoomCardInfo roomCardInfo;
    private static readonly string FIELD_CONSTRUCTION_ROOM_PREFAB = "Prefabs/FieldConstructionRoom";

    public int GetTimer() { return timer; }

    public override Room GetFieldRoomPrefab()
    {
        return Resources.Load<Room>(FIELD_CONSTRUCTION_ROOM_PREFAB);
    }
}
