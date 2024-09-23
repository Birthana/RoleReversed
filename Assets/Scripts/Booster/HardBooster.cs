using UnityEngine;

[CreateAssetMenu(fileName = "HardBooster", menuName = "BoosterInfo/HardBooster")]
public class HardBooster : BoosterInfo
{
    public override void CreatePack()
    {
        var packPrefab = Resources.Load<Pack>("Prefabs/Pack");
        var pack = Instantiate(packPrefab);
        pack.LoadRandomHardCard();
        pack.LoadRandomHardCard();
        pack.LoadRandomHardCard();
        pack.LoadRandomHardCard();
        pack.LoadRandomHardCard();
    }
}
