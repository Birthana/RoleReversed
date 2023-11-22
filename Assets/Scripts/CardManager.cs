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

    private Card CreateCard(CardInfo cardInfo)
    {
        var newCard = CreateNewCard(cardInfo);
        newCard.SetCardInfo(cardInfo);
        return newCard;
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
            if (cardToCheck.cardName.Equals(card.cardName))
            {
                return true;
            }
        }

        return false;
    }

    public Card CreateMonsterCard() { return CreateSpecificCard(CardInfoIsLowCostMonster); }

    private bool CardInfoIsLowCostMonster(CardInfo cardInfo) { return cardInfo.IsMonster() && (cardInfo.cost < 3); }

    public Card CreateRoomCard() { return CreateSpecificCard(CardInfoIsLowCostRoom); }

    private bool CardInfoIsLowCostRoom(CardInfo cardInfo) { return cardInfo.IsRoom() && (cardInfo.cost < 3); }

    private Card CreateSpecificCard(Func<CardInfo, bool> requirementFunction)
    {
        CardInfo card = null;
        do
        {
            card = GetValidCard(requirementFunction);
        } while (card == null);

        return CreateCard(card);
    }

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

// TODO: Condense monster/rooms into the Card Scriptable by making the specific ScriptableObject for each monster/room.
// TODO: Polish Health & Damage Numbers
// TODO: Animation: Zoom into Room that Battle is happening