using UnityEngine;

[CreateAssetMenu(fileName = "TooltipInfo", menuName = "TooltipInfo")]
public class TooltipInfo : ScriptableObject
{
    public Tag tag;
    [TextArea(3, 5)]
    public string description;
    public int fontSize = 6;
}
