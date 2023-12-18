using System.Collections.Generic;
using UnityEngine;

public class Pack : MonoBehaviour
{
    public int numberOfCards = 5;
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

    public void CreateRarityPack()
    {
        var rngCards = FindObjectOfType<CardManager>();
        cards.Add(rngCards.CreateRareCard());
        cards.Add(rngCards.CreateRoomCard());
        cards.Add(rngCards.CreateMonsterCard());
        for (int i = 0; i < numberOfCards - 3; i++)
        {
            cards.Add(rngCards.CreateCommonCard());
        }
    }

    public void CreateStarterPack()
    {
        var rngCards = FindObjectOfType<CardManager>();
        do
        {
            cards = new List<Card>();
            cards.Add(rngCards.CreateRoomCard());
            cards.Add(rngCards.CreateMonsterCard());
        } while (GetTotalCost() > 3);
    }

    public void Update()
    {
        if (PlayerClicksOnPack())
        {
            CreateStarterPack();
            OpenPack();
            DestroyImmediate(gameObject);
        }
    }

    public int GetSize() { return cards.Count; }

    private void OpenPack()
    {
        var hand = FindObjectOfType<Hand>();
        foreach (var card in cards)
        {
            hand.Add(card);
        }
    }

    private bool PlayerClicksOnPack() { return mouseWrapper.PlayerPressesLeftClick() && mouseWrapper.IsOnPack(); }
}
