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

    public Card CreateRandomCard() { return CreateCard(GetRandomCard()); }

    public Card CreateCommonCard() { return CreateCard(GetCommonCard()); }

    public Card CreateRareCard() { return CreateCard(GetRareCard()); }

    public Card CreateCard(CardInfo cardInfo)
    {
        var newCard = CreateNewCard(cardInfo);
        newCard.SetCardInfo(cardInfo);
        return newCard;
    }

    public List<Card> CreateCards(List<CardInfo> cardInfos)
    {
        var cards = new List<Card>();
        foreach (var cardInfo in cardInfos)
        {
            cards.Add(CreateCard(cardInfo));
        }

        return cards;
    }

    private Card CreateNewCard(CardInfo cardInfo) { return Instantiate(GetCardPrefab(cardInfo), transform); }

    private Card GetCardPrefab(CardInfo cardInfo)
    {
        if (cardInfo.IsMonster())
        {
            return monsterCardPrefab;
        }

        return roomCardPrefab;
    }

    public bool CardIsCommon(Card card) { return ListContainsCard(commons, card); }

    public bool CardIsRare(Card card) { return ListContainsCard(rares, card); }

    private bool ListContainsCard(List<CardInfo> cards, Card cardToCheck)
    {
        foreach (var card in cards)
        {
            if (cardToCheck.GetName().Equals(card.cardName))
            {
                return true;
            }
        }

        return false;
    }

    public Card CreateMonsterCard() { return CreateSpecificCard(CardInfoIsLowCostMonster); }

    public bool CardInfoIsLowCostMonster(CardInfo cardInfo) { return cardInfo.IsMonster() && (cardInfo.cost < 3); }

    public Card CreateRoomCard() { return CreateSpecificCard(CardInfoIsLowCostRoom); }

    public bool CardInfoIsLowCostRoom(CardInfo cardInfo) { return cardInfo.IsRoom() && (cardInfo.cost < 3); }

    private Card CreateSpecificCard(Func<CardInfo, bool> requirementFunction)
    {
        var cardInfo = GetValidCardInfo(requirementFunction);
        return CreateCard(cardInfo);
    }

    public CardInfo GetValidCardInfo(Func<CardInfo, bool> requirementFunction)
    {
        CardInfo cardInfo = null;
        do
        {
            cardInfo = GetValidCard(requirementFunction);
        } while (cardInfo == null);

        return cardInfo;
    }

    public CardInfo GetValidCard(Func<CardInfo, bool> requirementFunction)
    {
        var rngCardInfo = GetRandomCard();
        if (requirementFunction(rngCardInfo))
        {
            return rngCardInfo;
        }

        return null;
    }

    public List<CardInfo> GetValidCardInfos(Func<List<CardInfo>, bool> requirementFunction)
    {
        List<CardInfo> cardInfos;
        do
        {
            cardInfos = GetValidStarterCardInfos();
        } while (requirementFunction(cardInfos));

        return cardInfos;
    }

    private List<CardInfo> GetValidStarterCardInfos()
    {
        var cardInfos = new List<CardInfo>();
        var newCardInfo = GetValidCardInfo(CardInfoIsLowCostRoom);
        cardInfos.Add(newCardInfo);
        newCardInfo = GetValidCardInfo(CardInfoIsLowCostMonster);
        cardInfos.Add(newCardInfo);
        return cardInfos;
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

// TODO: Add soul shop.
// TODO: Refactor DraftManager.cs
// TODO: Add soul mechnanic.
// TODO: Add new cards.
// TODO: Clean up MonsterDraggerTests Mock Setups.
// TODO: Animation: Zoom into Room that Battle is happening
// TODO: Bug: Release with no card selected in CardDragger.cs