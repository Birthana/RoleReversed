using UnityEngine;

public class SkillInfo : ScriptableObject
{
    public Sprite sprite;
    public string description;

    public virtual void Cast(Room room) { }
}
