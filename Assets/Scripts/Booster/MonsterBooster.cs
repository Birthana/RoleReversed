using UnityEngine;

[CreateAssetMenu(fileName = "BoosterInfo", menuName = "BoosterInfo/MonsterBooster")]
public class MonsterBooster : BoosterInfo
{
    public Pack packPrefab;

    public override void CreatePack()
    {
        var pack = Instantiate(packPrefab);
        pack.LoadRandomMonsterThatCostOne();
        pack.LoadRandomMonsterThatCostTwo();
        pack.LoadRandomRoom();
    }
}
