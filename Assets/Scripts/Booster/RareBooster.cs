using UnityEngine;

[CreateAssetMenu(fileName = "BoosterInfo", menuName = "BoosterInfo/RareBooster")]
public class RareBooster : BoosterInfo
{
    public Pack packPrefab;

    public override void CreatePack()
    {
        var pack = Instantiate(packPrefab);
        pack.LoadRandomRarePack();
        pack.LoadRandomMonster();
        pack.LoadRandomMonster();
        pack.LoadRandomRoom();
        pack.LoadRandomRoom();
    }
}
