using UnityEngine;

[CreateAssetMenu(menuName = "Option/RarityPack", fileName = "RarityPack")]
public class RandomRare : OptionInfo
{
    public Pack packPrefab;

    public override void Choose()
    {
        var pack = Instantiate(packPrefab);
        pack.LoadRandomRarePack();
        base.Choose();
    }
}
