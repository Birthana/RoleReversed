using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    private static readonly string RARE_CARDS_FILE_PATH = "Prefabs/Rares";
    private static readonly string EASY_FILE_PATH = "Prefabs/Easy";
    private static readonly string MEDIUM_CARDS_FILE_PATH = "Prefabs/Medium";
    private static readonly string HARD_FILE_PATH = "Prefabs/Hard";
    private List<CardInfo> rares = new List<CardInfo>();
    private List<CardInfo> easy = new List<CardInfo>();
    private List<CardInfo> medium = new List<CardInfo>();
    private List<CardInfo> hard = new List<CardInfo>();
    private bool mediumCardsUnlocked = false;
    private bool hardCardsUnlocked = false;
    private List<GameObject> unlockWhenMediumCards = new List<GameObject>();
    private List<GameObject> unlockWhenHardCards = new List<GameObject>();

    public void Awake()
    {
        LoadCards(RARE_CARDS_FILE_PATH, AddRareCard);
        LoadCards(EASY_FILE_PATH, AddEasyCard);
        LoadCards(MEDIUM_CARDS_FILE_PATH, AddMediumCard);
        LoadCards(HARD_FILE_PATH, AddHardCard);
    }

    public void AddToMediumCardsUnlock(GameObject unlockObject) { unlockWhenMediumCards.Add(unlockObject); }
    public void AddToHardCardsUnlock(GameObject unlockObject) { unlockWhenHardCards.Add(unlockObject); }

    public void UnlockMediumCards()
    {
        mediumCardsUnlocked = true;
        UnlockObjects(unlockWhenMediumCards);
    }

    public void UnlockHardCards()
    {
        hardCardsUnlocked = true;
        UnlockObjects(unlockWhenHardCards);
    }

    private void UnlockObjects(List<GameObject> unlockObjects)
    {
        foreach (var unlock in unlockObjects)
        {
            unlock.SetActive(true);
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

    public void AddRareCard(CardInfo card) { rares.Add(card); }
    public void AddEasyCard(CardInfo card) { easy.Add(card); }
    public void AddMediumCard(CardInfo card) { medium.Add(card); }
    public void AddHardCard(CardInfo card) { hard.Add(card); }

    public bool CardIsRare(Card card) { return ListContainsCard(rares, card); }
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

    public CardInfo GetValidCard(Func<CardInfo, bool> requirementFunction)
    {
        var rngCardInfo = GetRandomCardInfo();
        if (requirementFunction(rngCardInfo))
        {
            return rngCardInfo;
        }

        return null;
    }

    public List<CardInfo> GetUniqueCardInfos(int numberOfDraftCards)
    {
        var cardInfos = new List<CardInfo>();
        for (int i = 0; i < numberOfDraftCards - 1; i++)
        {
            cardInfos.Add(GetUniqueCommonCardInfo(cardInfos));
        }

        cardInfos.Add(GetUniqueRandomCardInfo(cardInfos));

        return cardInfos;
    }

    private CardInfo GetUniqueCommonCardInfo(List<CardInfo> cardInfos)
    {
        CardInfo cardInfo;
        do
        {
            cardInfo = GetEasyCardInfo();
        } while (CardInfoIsNotUnique(cardInfo, cardInfos));

        return cardInfo;
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

    public CardInfo GetRandomCardInfo()
    {
        int rngIndex = UnityEngine.Random.Range(0, 3);

        if (rngIndex == 0 && hardCardsUnlocked)
        {
            return GetHardCardInfo();
        }

        if (rngIndex == 1 && mediumCardsUnlocked)
        {
            return GetMediumCardInfo();
        }

        return GetEasyCardInfo();
    }

    public CardInfo GetEasyCardInfo()
    {
        int rngIndex = UnityEngine.Random.Range(0, easy.Count);
        return easy[rngIndex];
    }

    public CardInfo GetMediumCardInfo()
    {
        int rngIndex = UnityEngine.Random.Range(0, medium.Count);
        return medium[rngIndex];
    }

    public CardInfo GetHardCardInfo()
    {
        int rngIndex = UnityEngine.Random.Range(0, hard.Count);
        return hard[rngIndex];
    }

    public CardInfo GetRareCardInfo()
    {
        int rngIndex = UnityEngine.Random.Range(0, rares.Count);
        return rares[rngIndex];
    }
}

//----------------
// Current Things
//----------------

// TODO: Fix Sprite Layers. Sorting Layers/Groups
// TODO: Add End Screen with Stats: Player Damage Dealt & Taken. Monster Damage Dealt & Monsters Died.

//----------------
// Features
//----------------
// TODO: Add cannot attack animation. Lerp Alpha.
// TODO: Add Action Room Effects.
// TODO: Add Option to remove a common card in deck to add a rare card to deck
// TODO: Add Transform Options.
// TODO: Add Monster Soul mechnanic.
// TODO: Add something to reward adding cards to Deck.
// TODO: Add incentive for getting a set of 4 cards.
// TODO: Add Move to Room mechnanics. (Blink and Knockback)
// TODO: Add Build Phase & Collection Box.

//----------------
// Refactors
//----------------
// TODO: Move Player is dead checks inside Player.cs.
// TODO: Remove cards in Pack.cs
// TODO: Make unittests for OptionInfos.
// TODO: Split UI and Mechanics of various scripts
// TODO: Create common array container for Hand, Deck, Drop, etc.
// TODO: Remove Temporary Monster if-statements.
// TODO: Null Object in OrangeSlime.cs
// TODO: Organize Different Managers in a GameObject.
// TODO: Change Mouse Raycast to look at component instead of Layer.

//----------------
// Bugs
//----------------
// TODO: BUG: Reroll Option cannot be previously chosen option.
// TODO: Fix Room Monsters Display when Monsters are dead.
// TODO: BUG: Player can get negative damage, and heal monsters.
// TODO: Bug: Release with no card selected in CardDragger.cs
// TODO: Bug: Game ends even if Temporary Monsters are alive.

//----------------
// Animations
//----------------
// TODO: Animation: Flip Animation.
// TODO: Animation: Drawing Cards from Deck. Opening Pack.
// TODO: Loot Animation for Pack.cs. When Open Start Pack.

//----------------
// Sprites
//----------------
// TODO: Create Shop Option BackGround.
// TODO: Create DisplayCard Background.
