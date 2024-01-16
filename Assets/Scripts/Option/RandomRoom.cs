using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Option/RandomRoom", fileName = "RandomRoom")]
public class RandomRoom : OptionInfo
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
