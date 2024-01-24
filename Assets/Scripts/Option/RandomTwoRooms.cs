using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Option/RandomTwoRooms", fileName = "RandomTwoRooms")]
public class RandomTwoRooms : OptionInfo
{
    public Pack packPrefab;

    public override void Choose()
    {
        var pack = Instantiate(packPrefab);
        pack.LoadRandomRoom();
        pack.LoadRandomRoom();
        base.Choose();
    }
}
