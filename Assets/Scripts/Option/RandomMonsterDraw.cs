using UnityEngine;

[CreateAssetMenu(menuName = "Option/RandomMonsterDraw", fileName = "RandomMonsterDraw")]
public class RandomMonsterDraw : OptionInfo
{
    public Pack packPrefab;

    public override void Choose()
    {
        var pack = Instantiate(packPrefab);
        pack.LoadRandomMonster();
        pack.SetDrawOnOpen();
        base.Choose();
    }
}
