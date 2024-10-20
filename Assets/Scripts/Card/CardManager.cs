using System;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    private static readonly string EASY_FILE_PATH = "Prefabs/Easy";
    private static readonly string MEDIUM_CARDS_FILE_PATH = "Prefabs/Medium";
    private static readonly string HARD_FILE_PATH = "Prefabs/Hard";
    private List<CardInfo> easy = new List<CardInfo>();
    private List<CardInfo> medium = new List<CardInfo>();
    private List<CardInfo> hard = new List<CardInfo>();
    private List<CardInfo> cardPool = new List<CardInfo>();

    public void Awake()
    {
        LoadCards(EASY_FILE_PATH, AddEasyCard);
        LoadCards(MEDIUM_CARDS_FILE_PATH, AddMediumCard);
        LoadCards(HARD_FILE_PATH, AddHardCard);
    }

    private void LoadCards(string path, Action<CardInfo> addToCardList)
    {
        var resourceCards = Resources.LoadAll<CardInfo>(path);
        foreach (var card in resourceCards)
        {
            var newCardInfo = Instantiate(card);
            addToCardList(newCardInfo);
        }
    }

    public void UnlockMediumCards()
    {
        AddToCardPool(medium);
    }

    public void UnlockHardCards()
    {
        AddToCardPool(hard);
    }

    public void UnlockCards(Tag tag)
    {
        AddToCardPool(easy, CardInfoIs, tag);
        AddToCardPool(medium, CardInfoIs, tag);
        AddToCardPool(hard, CardInfoIs, tag);
    }

    private void AddToCardPool(List<CardInfo> cards)
    {
        foreach (var card in cards)
        {
            AddToCardPool(card);
        }
    }

    private void AddToCardPool(List<CardInfo> cards, Func<CardInfo,Tag, bool> requirementFunction, Tag tag)
    {
        foreach (var card in cards)
        {
            if (requirementFunction(card, tag))
            {
                AddToCardPool(card);
            }
        }
    }

    private void AddToCardPool(CardInfo card)
    {
        if (cardPool.Contains(card))
        {
            return;
        }

        cardPool.Add(card);
    }

    public void AddEasyCard(CardInfo card)
    {
        cardPool.Add(card);
        easy.Add(card);
    }

    public void AddMediumCard(CardInfo card) { medium.Add(card); }
    public void AddHardCard(CardInfo card) { hard.Add(card); }

    public bool CardIsEasy(Card card) { return ListContainsCard(easy, card); }
    public bool CardIsMedium(Card card) { return ListContainsCard(medium, card); }
    public bool CardIsHard(Card card) { return ListContainsCard(hard, card); }

    private bool ListContainsCard(List<CardInfo> cards, Card cardToCheck)
    {
        return CheckCardNameIsSame(cardToCheck.GetName(), cards);
    }

    private bool ListContainsCard(List<CardInfo> cards, CardInfo cardToCheck)
    {
        return CheckCardNameIsSame(cardToCheck.cardName, cards);
    }

    private bool CheckCardNameIsSame(string cardName, List<CardInfo> cards)
    {
        foreach (var card in cards)
        {
            if (cardName.Equals(card.cardName))
            {
                return true;
            }
        }

        return false;
    }

    public bool CardInfoIsLowCostMonster(CardInfo cardInfo)
    {
        return CardInfoIsMonster(cardInfo) && (cardInfo.cost < 3) && ListContainsCard(easy, cardInfo) 
                && !(cardInfo is ConstructionRoomCardInfo);
    }

    public bool CardInfoIsMonster(CardInfo cardInfo) { return cardInfo is MonsterCardInfo; }

    public bool CardInfoIsMonsterAndCostOne(CardInfo cardInfo)
    {
        return CardInfoIsMonster(cardInfo, 1);
    }

    public bool CardInfoIsMonsterAndCostTwo(CardInfo cardInfo)
    {
        return CardInfoIsMonster(cardInfo, 2);
    }

    private bool CardInfoIsMonster(CardInfo cardInfo, int cost) { return cardInfo is MonsterCardInfo && (cardInfo.cost == cost); }

    public bool CardInfoIs(CardInfo cardInfo, Tag tag) { return cardInfo.tags.Contains(tag); }

    public bool CardInfoIsLowCostRoom(CardInfo cardInfo)
    {
        return CardInfoIsRoom(cardInfo) && (cardInfo.cost < 3) && ListContainsCard(easy, cardInfo)
                && !(cardInfo is ConstructionRoomCardInfo);
    }

    public bool CardInfoIsRoom(CardInfo cardInfo) { return cardInfo.IsRoom(); }

    public bool CardInfoIsConstructionRoom(CardInfo cardInfo)
    {
        return cardInfo is ConstructionRoomCardInfo;
    }

    public bool CardInfoIsEasy(CardInfo cardInfo) { return ListContainsCard(easy, cardInfo); }

    public bool CardInfoIsMedium(CardInfo cardInfo) { return ListContainsCard(medium, cardInfo); }

    public bool CardInfoIsHard(CardInfo cardInfo) { return ListContainsCard(hard, cardInfo); }

    public CardInfo GetValidCardInfo(Func<CardInfo, bool> requirementFunction)
    {
        CardInfo cardInfo = null;
        do
        {
            cardInfo = GetValidCard(requirementFunction);
        } while (cardInfo == null);

        return cardInfo;
    }

    public CardInfo GetValidCardInfo(Func<CardInfo, Tag, bool> requirementFunction, Tag tag)
    {
        CardInfo cardInfo = null;
        do
        {
            cardInfo = GetValidCard(requirementFunction, tag);
        } while (cardInfo == null);

        return cardInfo;
    }

    public CardInfo GetValidCard(Func<CardInfo, bool> requirementFunction)
    {
        var rngCardInfo = GetRandomCardInfo();
        if (requirementFunction(rngCardInfo))
        {
            return rngCardInfo;
        }

        return null;
    }

    public CardInfo GetValidCard(Func<CardInfo, Tag, bool> requirementFunction, Tag tag)
    {
        var rngCardInfo = GetRandomCardInfo();
        if (requirementFunction(rngCardInfo, tag))
        {
            return rngCardInfo;
        }

        return null;
    }

    public List<CardInfo> GetUniqueCardInfos(int numberOfDraftCards)
    {
        var cardInfos = new List<CardInfo>();
        for (int i = 0; i < numberOfDraftCards; i++)
        {
            cardInfos.Add(GetUniqueRandomCardInfo(cardInfos));
        }

        return cardInfos;
    }

    public List<CardInfo> GetUniqueCardInfos(int numberOfDraftCards, Tag tag)
    {
        var cardInfos = new List<CardInfo>();
        for (int i = 0; i < numberOfDraftCards; i++)
        {
            cardInfos.Add(GetUniqueRandomCardInfo(cardInfos, tag));
        }

        return cardInfos;
    }

    private CardInfo GetUniqueRandomCardInfo(List<CardInfo> cardInfos)
    {
        CardInfo cardInfo;
        do
        {
            cardInfo = GetRandomCardInfo();
        } while (CardInfoIsNotUnique(cardInfo, cardInfos));

        return cardInfo;
    }

    private CardInfo GetUniqueRandomCardInfo(List<CardInfo> cardInfos, Tag tag)
    {
        CardInfo cardInfo;
        do
        {
            cardInfo = GetRandomCardInfo();
        } while (CardInfoIsNotUnique(cardInfo, cardInfos) || !CardInfoIs(cardInfo, tag));

        return cardInfo;
    }


    private CardInfo GetUniqueRandomCardInfo(Func<CardInfo, bool> requirementFunction, List<CardInfo> cardInfos)
    {
        CardInfo cardInfo;
        do
        {
            cardInfo = GetRandomCardInfo();
        } while (CardInfoIsNotUnique(cardInfo, cardInfos) || !requirementFunction(cardInfo));

        return cardInfo;
    }

    private bool CardInfoIsNotUnique(CardInfo currentCardInfo, List<CardInfo> cardInfos)
    {
        if (cardInfos.Count == 0)
        {
            return false;
        }

        foreach (var cardInfo in cardInfos)
        {
            if (cardInfo.Equals(currentCardInfo))
            {
                return true;
            }
        }

        return false;
    }

    public List<CardInfo> GetValidStarterCardInfos(Func<List<CardInfo>, bool> requirementFunction)
    {
        List<CardInfo> cardInfos;
        do
        {
            cardInfos = new List<CardInfo>();
            cardInfos.Add(GetUniqueRandomCardInfo(CardInfoIsLowCostRoom, cardInfos));
            cardInfos.Add(GetUniqueRandomCardInfo(CardInfoIsLowCostMonster, cardInfos));
            cardInfos.Add(GetUniqueRandomCardInfo(CardInfoIsLowCostRoom, cardInfos));
            cardInfos.Add(GetUniqueRandomCardInfo(CardInfoIsLowCostMonster, cardInfos));
            cardInfos.Add(GetUniqueRandomCardInfo(CardInfoIsEasy, cardInfos));
        } while (requirementFunction(cardInfos));

        return cardInfos;
    }

    public CardInfo GetEasyCardInfo()
    {
        int rngIndex = UnityEngine.Random.Range(0, easy.Count);
        return Instantiate(easy[rngIndex]);
    }

    public CardInfo GetMediumCardInfo()
    {
        int rngIndex = UnityEngine.Random.Range(0, medium.Count);
        return Instantiate(medium[rngIndex]);
    }

    public CardInfo GetHardCardInfo()
    {
        int rngIndex = UnityEngine.Random.Range(0, hard.Count);
        return Instantiate(hard[rngIndex]);
    }

    public CardInfo GetRandomCardInfo()
    {
        int rngIndex = UnityEngine.Random.Range(0, cardPool.Count);
        return Instantiate(cardPool[rngIndex]);
    }
}
