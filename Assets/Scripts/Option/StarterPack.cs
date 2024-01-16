using UnityEngine;

[CreateAssetMenu(menuName = "Option/StarterPack", fileName = "StarterPack")]
public class StarterPack : OptionInfo
{
    public Pack packPrefab;

    public override void Choose()
    {
        var pack = Instantiate(packPrefab);
        pack.LoadBasicPack();
        base.Choose();
    }
}
