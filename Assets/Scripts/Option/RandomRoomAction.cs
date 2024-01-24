using UnityEngine;

[CreateAssetMenu(menuName = "Option/RandomRoomAction", fileName = "RandomRoomAction")]
public class RandomRoomAction : OptionInfo
{
    public Pack packPrefab;

    public override void Choose()
    {
        var pack = Instantiate(packPrefab);
        pack.LoadRandomRoom();
        pack.SetGainActionOnOpen();
        base.Choose();
    }
}
