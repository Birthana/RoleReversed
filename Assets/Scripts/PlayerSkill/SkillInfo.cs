using System.Collections;
using UnityEngine;

public class SkillInfo : ScriptableObject
{
    public int timer;
    public Sprite sprite;
    public string description;

    public virtual IEnumerator Cast(Room room) { yield return null; }
}
