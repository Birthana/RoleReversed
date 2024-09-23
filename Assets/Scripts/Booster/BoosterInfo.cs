using UnityEngine;

[CreateAssetMenu(fileName = "BoosterInfo", menuName = "BoosterInfo/BoosterInfo")]
public class BoosterInfo : ScriptableObject
{
    [TextArea(3, 5)]
    public string description;

    public virtual void CreatePack() { }
}
