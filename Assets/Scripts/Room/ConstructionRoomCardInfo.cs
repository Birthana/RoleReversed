using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRoomCard", menuName = "CardInfo/ConstructionRoom")]
public class ConstructionRoomCardInfo : RoomCardInfo
{
    private static readonly string FIELD_CONSTRUCTION_ROOM_PREFAB = "Prefabs/FieldConstructionRoom";

    public override Room GetFieldRoomPrefab()
    {
        return Resources.Load<Room>(FIELD_CONSTRUCTION_ROOM_PREFAB);
    }
}
