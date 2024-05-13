using System.Collections;
using UnityEngine;

public class RoommateEffectInfo : ScriptableObject
{
    [TextArea(3, 5)]
    public string cardDescription;

    public virtual IEnumerator BattleStart(Room room)
    {
        yield return null;
    }
}
