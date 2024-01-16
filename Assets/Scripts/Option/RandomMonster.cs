using UnityEngine;

[CreateAssetMenu(menuName = "Option/RandomMonster", fileName = "RandomMonster")]
public class RandomMonster : OptionInfo
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
