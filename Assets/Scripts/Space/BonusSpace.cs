public class BonusSpace : SpaceToBuild
{
    private SpaceInfo spaceInfo;

    public void Setup(SpaceInfo info) { spaceInfo = info; }

    public override void BuildEffect()
    {
        spaceInfo.BuildEffect();
    }
}
