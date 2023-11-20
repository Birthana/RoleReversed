using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public MonsterCard monsterCardPrefab;
    public RoomCard roomCardPrefab;
    private static readonly string COMMON_CARDS_FILE_PATH = "Prefabs/Commons";
    private static readonly string RARE_CARDS_FILE_PATH = "Prefabs/Rares";
    private List<CardInfo> commons = new List<CardInfo>();
    private List<CardInfo> rares = new List<CardInfo>();

    private void Awake()
    {
        LoadCards(COMMON_CARDS_FILE_PATH, AddCommonCard);
        LoadCards(RARE_CARDS_FILE_PATH, AddRareCard);
    }

    private void LoadCards(string path, Action<CardInfo> addToCardList)
    {
        var resourceCards = Resources.LoadAll<CardInfo>(path);
        foreach (var card in resourceCards)
        {
            addToCardList(card);
        }
    }

    public void AddCommonCard(CardInfo card) { commons.Add(card); }

    public void AddRareCard(CardInfo card) { rares.Add(card); }

    public Card CreateRandomCard()
    {
        var cardInfo = GetRandomCard();
        var newCard = CreateNewCard(cardInfo);
        newCard.SetCardInfo(cardInfo);
        return newCard;
    }

    public Card CreateCommonCard()
    {
        var cardInfo = GetCommonCard();
        var newCard = CreateNewCard(cardInfo);
        newCard.SetCardInfo(cardInfo);
        return newCard;
    }

    public Card CreateRareCard()
    {
        var cardInfo = GetRareCard();
        var newCard = CreateNewCard(cardInfo);
        newCard.SetCardInfo(cardInfo);
        return newCard;
    }

    private Card CreateNewCard(CardInfo cardInfo)
    {
        Card newCard;
        if (cardInfo.IsMonster())
        {
            newCard = Instantiate(monsterCardPrefab, transform);
        }
        else
        {
            newCard = Instantiate(roomCardPrefab, transform);
        }

        return newCard;
    }

    public bool CardIsCommon(Card card) { return ListContainsCard(commons, card); }

    public bool CardIsRare(Card card) { return ListContainsCard(rares, card); }

    private bool ListContainsCard(List<CardInfo> cards, Card cardToCheck)
    {
        foreach (var card in cards)
        {
            if (cardToCheck.cardName.Equals(card.cardName))
            {
                return true;
            }
        }

        return false;
    }

    public Card CreateMonsterCard()
    {
        CardInfo card = null;
        do
        {
            card = GetValidCard(CardInfoIsLowCostMonster);
        } while (card == null);

        var newCard = Instantiate(monsterCardPrefab, transform);
        newCard.SetCardInfo(card);
        return newCard;
    }

    private bool CardInfoIsLowCostMonster(CardInfo cardInfo) { return cardInfo.IsMonster() && (cardInfo.cost < 3); }

    public Card CreateRoomCard()
    {
        CardInfo card = null;
        do
        {
            card = GetValidCard(CardInfoIsLowCostRoom);
        } while (card == null);

        var newCard = Instantiate(roomCardPrefab, transform);
        newCard.SetCardInfo(card);
        return newCard;
    }

    private bool CardInfoIsLowCostRoom(CardInfo cardInfo) { return cardInfo.IsRoom() && (cardInfo.cost < 3); }

    private CardInfo GetValidCard(Func<CardInfo, bool> requirementFunction)
    {
        var rngCardInfo = GetRandomCard();
        if (requirementFunction(rngCardInfo))
        {
            return rngCardInfo;
        }

        return null;
    }

    private CardInfo GetRandomCard()
    {
        int rngIndex = UnityEngine.Random.Range(0, 2);
        if (rngIndex == 0)
        {
            return GetCommonCard();
        }

        return GetRareCard();
    }

    private CardInfo GetCommonCard()
    {
        int rngIndex = UnityEngine.Random.Range(0, commons.Count);
        return commons[rngIndex];
    }

    private CardInfo GetRareCard()
    {
        int rngIndex = UnityEngine.Random.Range(0, rares.Count);
        return rares[rngIndex];
    }
}

// TODO: Name unittests
// TODO: Extract set Card description in Card.cs
// TODO: Condense monster/rooms