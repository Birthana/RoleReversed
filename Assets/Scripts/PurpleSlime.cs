using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleSlime : Monster
{
    public override void Entrance()
    {
        FindObjectOfType<Player>().ReduceDamage(1);
    }
}
