using UnityEngine;

[CreateAssetMenu(fileName = "BoosterInfo", menuName = "BoosterInfo/BoosterInfo")]
public class BoosterInfo : ScriptableObject
{
    public Sprite sprite;
    public int cost;
    [TextArea(3, 5)]
    public string description;

    public virtual void CreatePack() { }
    public virtual void Unlock() { }
}
