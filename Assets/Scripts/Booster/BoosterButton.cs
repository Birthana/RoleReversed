using System;
using UnityEngine;

public class BoosterButton : MonoBehaviour
{
    public event Action<int> OnSoulCostChange;
    private BoosterInfo boosterInfo;

    public void Setup(BoosterInfo info)
    {
        boosterInfo = info;
        GetComponent<SpriteRenderer>().sprite = info.sprite;
        OnSoulCostChange += GetComponent<BasicUI>().Display;
        OnSoulCostChange?.Invoke(boosterInfo.cost);
    }

    private void OnMouseEnter()
    {
        FindObjectOfType<BoosterToolTip>().Display(boosterInfo.name, boosterInfo.description);
        FindObjectOfType<BoosterToolTip>().MoveTo(transform.position);
    }

    private void OnMouseExit()
    {
        FindObjectOfType<BoosterToolTip>().Hide();
    }

    private void OnMouseDown()
    {
        var playerSouls = FindObjectOfType<PlayerSoulCounter>();
        if (playerSouls.GetCurrentSouls() < boosterInfo.cost)
        {
            return;
        }

        FindObjectOfType<SoulShop>().CloseShop();
        FindObjectOfType<BoosterToolTip>().Hide();
        playerSouls.DecreaseSouls(boosterInfo.cost);
        boosterInfo.CreatePack();
    }
}
