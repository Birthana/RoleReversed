using System;
using System.Collections.Generic;
using UnityEngine;

public class Pack : MonoBehaviour
{
    public int numberOfCards = 5;
    private event Action OnOpenPack;
    private List<CardInfo> cardInfos = new List<CardInfo>();
    private List<Card> cards = new List<Card>();
    private IMouseWrapper mouseWrapper;

    public void SetMouseWrapper(IMouseWrapper wrapper)
    {
        mouseWrapper = wrapper;
    }

    private void Awake()
    {
        SetMouseWrapper(new MouseWrapper());
    }

    public int GetTotalCost()
    {
        var totalCost = 0;
        foreach(var card in cards)
        {
            totalCost += card.GetCost();
        }

        return totalCost;
    }

    public void LoadStarterPack()
    {
        var rngCards = FindObjectOfType<CardManager>();
        cardInfos = rngCards.GetValidStarterCardInfos(TotalCostIsGreaterThanThree);
        SetDrawOnOpen();
        SetDrawOnOpen();
    }

    public void LoadBasicPack()
    {
        var rngCards = FindObjectOfType<CardManager>();
        cardInfos = rngCards.GetValidStarterCardInfos(TotalCostIsGreaterThanThree);
    }

    public void LoadRandomRarePack()
    {
        var rngCards = FindObjectOfType<CardManager>();
        cardInfos.Add(rngCards.GetRareCardInfo());
    }

    public void CreateStarterPack()
    {
        LoadStarterPack();
        CreatePackToHand();
    }

    private void CreatePackToHand()
    {
        var deck = FindObjectOfType<Deck>();
        foreach (var cardInfo in cardInfos)
        {
            deck.Add(cardInfo);
        }
    }

    public void LoadRandomMonster()
    {
        var rngCards = FindObjectOfType<CardManager>();
        cardInfos.Add(rngCards.GetValidCardInfo(rngCards.CardInfoIsMonster));
    }

    public void LoadRandomRoom()
    {
        var rngCards = FindObjectOfType<CardManager>();
        cardInfos.Add(rngCards.GetValidCardInfo(rngCards.CardInfoIsRoom));
    }

    private bool TotalCostIsGreaterThanThree(List<CardInfo> cardInfos)
    {
        return GetTotalCost(cardInfos) > 3;
    }

    private int GetTotalCost(List<CardInfo> cardInfos)
    {
        var totalCost = 0;
        foreach (var cardInfo in cardInfos)
        {
            totalCost += cardInfo.cost;
        }

        return totalCost;
    }

    public void Update()
    {
        if (PlayerClicksOnAPack())
        {
            if(PackIsNotClicked())
            {
                return;
            }

            OpenPack();
        }
    }

    private void OpenPack()
    {
        CreatePackToHand();
        AddCardsToHand();
        OnOpenPack?.Invoke();
        DestroyImmediate(gameObject);
    }

    public void SetDrawOnOpen() { OnOpenPack += DrawCard; }

    private void DrawCard()
    {
        FindObjectOfType<Deck>().DrawCardToHand();
        OnOpenPack -= DrawCard;
    }

    private bool PackIsNotClicked()
    {
        var clickedPack = mouseWrapper.GetHitComponent<Pack>();
        if (clickedPack == null)
        {
            return true;
        }

        return !PackIsSame(clickedPack);
    }

    private bool PackIsSame(Pack clickedPack)
    {
        return clickedPack.gameObject.Equals(gameObject);
    }

    public int GetSize() { return cardInfos.Count; }

    private void AddCardsToHand()
    {
        var hand = FindObjectOfType<Hand>();
        foreach (var card in cards)
        {
            hand.Add(card);
        }
    }

    private bool PlayerClicksOnAPack() { return mouseWrapper.PlayerPressesLeftClick() && mouseWrapper.IsOnPack(); }
}
