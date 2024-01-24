using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Option/RandomRoomDraw", fileName = "RandomRoomDraw")]
public class RandomRoomDraw : OptionInfo
{
    public Pack packPrefab;

    public override void Choose()
    {
        var pack = Instantiate(packPrefab);
        pack.LoadRandomRoom();
        pack.SetDrawOnOpen();
        base.Choose();
    }
}
