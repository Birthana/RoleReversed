using UnityEngine;

[CreateAssetMenu(fileName = "BoosterInfo", menuName = "BoosterInfo/RoomBooster")]
public class RoomBooster : BoosterInfo
{
    public Pack packPrefab;

    public override void CreatePack()
    {
        var pack = Instantiate(packPrefab);
        pack.LoadRandomConstructionRoom();
        pack.LoadRandomConstructionRoom();
        pack.LoadRandomMonster();
    }
}
