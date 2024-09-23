using UnityEngine;

[CreateAssetMenu(fileName = "BoosterInfo", menuName = "BoosterInfo/MonsterBooster")]
public class MonsterBooster : BoosterInfo
{
    public override void CreatePack()
    {
        var packPrefab = Resources.Load<Pack>("Prefabs/Pack");
        var pack = Instantiate(packPrefab);
        pack.LoadRandomMonsterThatCostOne();
        pack.LoadRandomMonsterThatCostTwo();
        pack.LoadRandomRoom();
    }
}
