using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public MonsterCard monsterCardPrefab;
    public RoomCard roomCardPrefab;
    private static readonly string COMMON_CARDS_FILE_PATH = "Prefabs/Commons";
    private static readonly string COMMON_CARDS_FILE_PATH_ = "Prefabs/Commons_";
    private static readonly string RARE_CARDS_FILE_PATH = "Prefabs/Rares";
    private List<Card> commons = new List<Card>();
    private List<Card> rares = new List<Card>();
    private List<CardInfo> commons_ = new List<CardInfo>();

    private void Awake()
    {
        LoadCards(COMMON_CARDS_FILE_PATH, AddCommonCard);
        LoadCards(COMMON_CARDS_FILE_PATH_, AddCommonCard_);
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

    private void LoadCards(string path, Action<CardInfo> addToCardList)
    {
        var resourceCards = Resources.LoadAll<CardInfo>(path);
        foreach (var card in resourceCards)
        {
            addToCardList(card);
        }
    }

    public void AddCommonCard(Card card)
    {
        commons.Add(card);
    }

    public void AddCommonCard_(CardInfo card)
    {
        commons_.Add(card);
    }

    public void AddRareCard(Card card)
    {
        rares.Add(card);
    }

    public Card CreateRandomCard() { return Instantiate(GetRandomCard()); }

    public Card CreateCommonCard()
    {
        var cardInfo = GetCommonCard_();
        var card = Instantiate(monsterCardPrefab);
        card.SetCardInfo(cardInfo);
        return card;
    }

    public Card CreateRareCard() { return Instantiate(GetRareCard()); }

    public bool CardIsCommon(Card card)
    {
        foreach (var common in commons_)
        {
            if (card.cardName.Equals(common.cardName))
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

    public Card CreateMonsterCard()
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

        return Instantiate(card);
    }

    public Card CreateRoomCard()
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

        return Instantiate(card);
    }

    private Card GetRandomCard()
    {
        int rngIndex = UnityEngine.Random.Range(0, 2);
        if (rngIndex == 0)
        {
            return GetCommonCard();
        }

        return GetRareCard();
    }

    private Card GetCommonCard()
    {
        int rngIndex = UnityEngine.Random.Range(0, commons.Count);
        return commons[rngIndex];
    }

    private CardInfo GetCommonCard_()
    {
        int rngIndex = UnityEngine.Random.Range(0, commons_.Count);
        return commons_[rngIndex];
    }

    private Card GetRareCard()
    {
        int rngIndex = UnityEngine.Random.Range(0, rares.Count);
        return rares[rngIndex];
    }
}
