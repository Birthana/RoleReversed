using System;
using UnityEngine;

public class BoosterButton : MonoBehaviour
{
    public event Action<int> OnSoulCostChange;
    public BoosterInfo boosterInfo;
    public int soulCost;
    public bool isLocked = false;
    public bool unlockOnMediumCards = false;
    public bool unlockOnHardCards = false;

    private void Awake()
    {
        OnSoulCostChange += GetComponent<BasicUI>().Display;
        OnSoulCostChange?.Invoke(soulCost);
        if (isLocked)
        {
            Lock();
        }

        AddToUnlock();
    }

    private void AddToUnlock()
    {
        if (unlockOnMediumCards)
        {
            FindObjectOfType<CardManager>().AddToMediumCardsUnlock(gameObject);
        }

        if (unlockOnHardCards)
        {
            FindObjectOfType<CardManager>().AddToHardCardsUnlock(gameObject);
        }
    }

    private void Lock() { gameObject.SetActive(false); }

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
        if (playerSouls.GetCurrentSouls() < soulCost)
        {
            return;
        }

        playerSouls.DecreaseSouls(soulCost);
        boosterInfo.CreatePack();
    }
}
