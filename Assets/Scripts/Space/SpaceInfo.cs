using UnityEngine;

public class SpaceInfo : ScriptableObject
{
    public Sprite sprite;
    public AnimationClip animation;

    public virtual void BuildEffect() { }
}
