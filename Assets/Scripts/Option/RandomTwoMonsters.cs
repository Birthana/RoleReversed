using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Option/RandomTwoMonsters", fileName = "RandomTwoMonsters")]
public class RandomTwoMonsters : OptionInfo
{
    public Pack packPrefab;

    public override void Choose()
    {
        var pack = Instantiate(packPrefab);
        pack.LoadRandomMonster();
        pack.LoadRandomMonster();
        base.Choose();
    }
}
