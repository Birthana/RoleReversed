using UnityEngine;

public class BonusSpace : SpaceToBuild
{
    private SpaceInfo spaceInfo;

    public void Setup(SpaceInfo info)
    {
        spaceInfo = info;
        GetComponent<SpriteRenderer>().sprite = info.sprite;
    }

    public override void BuildEffect()
    {
        spaceInfo.BuildEffect();
    }
}
