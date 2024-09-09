using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "ActionRoom", menuName = "RoomCardInfo/ActionRoom")]
public class ActionRoom : RoomCardInfo
{
    public override IEnumerator BuildStart(Room room)
    {
        FindObjectOfType<ActionManager>().AddActions(1);
        yield return null;
    }
}
