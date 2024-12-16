using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BoosterButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public event Action<int> OnSoulCostChange;
    [SerializeField] private Image image;
    private BoosterInfo boosterInfo;

    public void Setup(BoosterInfo info)
    {
        boosterInfo = info;
        image.sprite = info.sprite;
        OnSoulCostChange += GetComponent<BasicUI>().Display;
        OnSoulCostChange?.Invoke(boosterInfo.cost);
    }

    public void BuyPack()
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        FindObjectOfType<BoosterToolTip>().Display(boosterInfo.name, boosterInfo.description);
        FindObjectOfType<BoosterToolTip>().MoveTo(transform.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        FindObjectOfType<BoosterToolTip>().Hide();
    }
}
