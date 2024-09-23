using UnityEngine;

[CreateAssetMenu(fileName = "BoosterInfo", menuName = "BoosterInfo/RoomBooster")]
public class RoomBooster : BoosterInfo
{
    public override void CreatePack()
    {
        var packPrefab = Resources.Load<Pack>("Prefabs/Pack");
        var pack = Instantiate(packPrefab);
        pack.LoadRandomConstructionRoom();
        pack.LoadRandomConstructionRoom();
        pack.LoadRandomMonster();
    }
}
