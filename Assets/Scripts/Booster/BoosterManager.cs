using System.Collections.Generic;
using UnityEngine;

public class BoosterManager : MonoBehaviour
{
    public List<Vector3> positions = new List<Vector3>();
    public BoosterButton boosterPrefab;
    private int count = 0;

    public void SpawnBooster(BoosterInfo info)
    {
        var booster = Instantiate(boosterPrefab, transform);
        booster.Setup(info);
        booster.transform.position = positions[count];
        count++;
    }
}
