using UnityEngine;

[CreateAssetMenu(fileName = "BoosterInfo", menuName = "BoosterInfo/RareBooster")]
public class RareBooster : BoosterInfo
{
    public override void CreatePack()
    {
        var packPrefab = Resources.Load<Pack>("Prefabs/Pack");
        var pack = Instantiate(packPrefab);
        pack.LoadRandomMonster();
        pack.LoadRandomMonster();
        pack.LoadRandomRoom();
        pack.LoadRandomRoom();
    }
}
