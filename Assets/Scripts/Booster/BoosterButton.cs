using System;
using UnityEngine;

public class BoosterButton : MonoBehaviour
{
    public event Action<int> OnSoulCostChange;
    public BoosterInfo boosterInfo;
    public int soulCost;

    private void Awake()
    {
        OnSoulCostChange += GetComponent<BasicUI>().Display;
        OnSoulCostChange?.Invoke(soulCost);
    }

    private void OnMouseDown()
    {
        var playerSouls = FindObjectOfType<PlayerSoulCounter>();
        if (playerSouls.GetCurrentSouls() < soulCost)
        {
            return;
        }

        playerSouls.DecreaseSouls(soulCost);
        boosterInfo.CreatePack();
    }
}
