using UnityEngine;

[CreateAssetMenu(fileName = "MediumBooster", menuName = "BoosterInfo/MediumBooster")]
public class MediumBooster : BoosterInfo
{
    public override void CreatePack()
    {
        var packPrefab = Resources.Load<Pack>("Prefabs/Pack");
        var pack = Instantiate(packPrefab);
        pack.LoadRandomMediumCard();
        pack.LoadRandomMediumCard();
        pack.LoadRandomMediumCard();
        pack.LoadRandomMediumCard();
        pack.LoadRandomMediumCard();
    }
}
