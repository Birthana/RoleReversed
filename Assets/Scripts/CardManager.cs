using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject cardPrefab;
    private static readonly string COMMON_CARDS_FILE_PATH = "Prefabs/Commons";
    private static readonly string RARE_CARDS_FILE_PATH = "Prefabs/Rares";
    private List<Card> commons = new List<Card>();
    private List<Card> rares = new List<Card>();
    private List<CardInfo> commons_ = new List<CardInfo>();
    private List<CardInfo> rares_ = new List<CardInfo>();

    private void Awake()
    {
        LoadCards(COMMON_CARDS_FILE_PATH, AddCommonCard);
        LoadCards(RARE_CARDS_FILE_PATH, AddRareCard);
    }

    private void LoadCards(string path, Action<Card> addToCardList)
    {
        var resourceCards = Resources.LoadAll<Card>(path);
        foreach (var card in resourceCards)
        {
            addToCardList(card);
        }
    }

    public void AddCommonCard(Card card)
    {
        commons.Add(card);
    }

    public void AddRareCard(Card card)
    {
        rares.Add(card);
    }

    public Card GetRandomCard()
    {
        int rngIndex = UnityEngine.Random.Range(0, 2);
        if (rngIndex == 0)
        {
            return GetCommonCard();
        }

        return GetRareCard();
    }

    public Card GetCommonCard()
    {
        int rngIndex = UnityEngine.Random.Range(0, commons.Count);
        return commons[rngIndex];
    }

    public Card GetRareCard()
    {
        int rngIndex = UnityEngine.Random.Range(0, rares.Count);
        return rares[rngIndex];
    }

    public bool CardIsCommon(Card card)
    {
        foreach(var common in commons)
        {
            if(card.Equals(common))
            {
                return true;
            }
        }

        return false;
    }

    public bool CardIsRare(Card card)
    {
        foreach (var rare in rares)
        {
            if (card.Equals(rare))
            {
                return true;
            }
        }

        return false;
    }

    public Card GetMonsterCard()
    {
        Card card = null;
        do
        {
            var rngCard = GetRandomCard();
            if(rngCard is MonsterCard && (rngCard.GetCost() < 3))
            {
                card = rngCard;
            }
        } while (card == null);

        return card;
    }

    public Card GetRoomCard()
    {
        Card card = null;
        do
        {
            var rngCard = GetRandomCard();
            if (rngCard is RoomCard && (rngCard.GetCost() < 3))
            {
                card = rngCard;
            }
        } while (card == null);

        return card;
    }
}
