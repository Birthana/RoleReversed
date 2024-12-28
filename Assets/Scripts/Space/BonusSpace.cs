using UnityEngine;

public class BonusSpace : SpaceToBuild
{
    private SpaceInfo spaceInfo;

    public void Setup(SpaceInfo info)
    {
        spaceInfo = info;
        GetComponent<SpriteRenderer>().sprite = info.sprite;
        if (info.animation == null)
        {
            return;
        }

        GetComponent<Animator>().Play(info.animation.name);
    }

    public override void BuildEffect()
    {
        spaceInfo.BuildEffect();
    }
}
