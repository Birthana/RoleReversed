using UnityEngine;

[CreateAssetMenu(menuName = "Option/RandomMonsterAction", fileName = "RandomMonsterAction")]
public class RandomMonsterAction : OptionInfo
{
    public Pack packPrefab;

    public override void Choose()
    {
        var pack = Instantiate(packPrefab);
        pack.LoadRandomMonster();
        pack.SetGainActionOnOpen();
        base.Choose();
    }
}
