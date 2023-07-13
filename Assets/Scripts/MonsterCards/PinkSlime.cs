using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkSlime : Monster
{
    public override void Entrance()
    {
        int roomCount = FindObjectsOfType<Room>().Length;
        IncreaseDamage(2 * roomCount);
        health.IncreaseMaxHealth(2 * roomCount);
    }
}
