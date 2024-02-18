using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [Range(5, 50)]
    public int rarityRate = 10;
    private static readonly string COMMON_CARDS_FILE_PATH = "Prefabs/Commons";
    private static readonly string RARE_CARDS_FILE_PATH = "Prefabs/Rares";
    private List<CardInfo> commons = new List<CardInfo>();
    private List<CardInfo> rares = new List<CardInfo>();

    public void Awake()
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

    public bool CardIsCommon(Card card) { return ListContainsCard(commons, card); }

    public bool CardIsRare(Card card) { return ListContainsCard(rares, card); }

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
        return CardInfoIsMonster(cardInfo) && (cardInfo.cost < 3) && ListContainsCard(commons, cardInfo);
    }

    public bool CardInfoIsMonster(CardInfo cardInfo) { return cardInfo.IsMonster(); }

    public bool CardInfoIsLowCostRoom(CardInfo cardInfo)
    {
        return CardInfoIsRoom(cardInfo) && (cardInfo.cost < 3) && ListContainsCard(commons, cardInfo);
    }

    public bool CardInfoIsRoom(CardInfo cardInfo) { return cardInfo.IsRoom(); }

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
            cardInfo = GetCommonCardInfo();
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

    public CardInfo GetRandomCardInfo()
    {
        int rngIndex = UnityEngine.Random.Range(0, Mathf.Max(1, 100 - rarityRate));
        if (rngIndex == 0)
        {
            return GetRareCardInfo();
        }

        return GetCommonCardInfo();
    }

    public CardInfo GetCommonCardInfo()
    {
        int rngIndex = UnityEngine.Random.Range(0, commons.Count);
        return commons[rngIndex];
    }

    public CardInfo GetRareCardInfo()
    {
        int rngIndex = UnityEngine.Random.Range(0, rares.Count);
        return rares[rngIndex];
    }

    public void IncreaseRarity() { rarityRate += 1; }
}

//----------------
// Current Things
//----------------
// TODO: Fix Orange Room & Pink Slime Interactions.
// TODO: Refactor EffectText.cs
// TODO: Make icons for Battle Start, Start Button, Entrance, Exit, Engage.
// TODO: Make Construction Room & Deck UI Sprites.
// TODO: Add Construction Room.

//----------------
// Features
//----------------
// TODO: Add Option to remove a common card in deck to add a rare card to deck
// TODO: Add Show BattleField Button In Draft Card Pick.
// TODO: Add Text Icons for card effects
// TODO: Add Tooltips
// TODO: Add Transform Options.
// TODO: Add Monster Soul mechnanic.
// TODO: Add new cards. (Blink/Reentrance trigger)
// TODO: Add something to reward adding cards to Deck.
// TODO: Add incentive for getting a set of 4 cards.
// TODO: Add incentive for getting more Rooms.

//----------------
// Refactors
//----------------
// TODO: Optiminize IconText to not for loop after first time Getting text.
// TODO: Clean up MonsterDraggerTests Mock Setups.
// TODO: Remove cards in Pack.cs
// TODO: Make unittests for OptionInfos.
// TODO: Split UI and Mechanics of various scripts
// TODO: Fix MonsterDraggerTest unittests.
// TODO: Refactor MonsterDragger.
// TODO: Clean/refactor GameManager WalkThru().
// TODO: Create common array container for Hand, Deck, Drop, etc.
// TODO: Remove Temporary Monster if-statements.
// TODO: Null Object in OrangeSlime.cs
// TODO: Organize Different Managers in a GameObject.
// TODO: Change CardTest to use Room to spawn Monsters.
// TODO: Change Mouse Raycast to look at component instead of Layer.
// TODO: Clean up LootAnimation.
// TODO: Combine Loot, DisplayCard, and DraftCard Prefabs.

//----------------
// Bugs
//----------------
// TODO: Fix commented out unittests in LootAnimation.cs
// TODO: Fix Room Monsters Display when Monsters are dead.
// TODO: Bug: Release with no card selected in CardDragger.cs
// TODO: BUG: Reroll Option cannot be previously chosen option.
// TODO: BUG: Player can get negative damage, and heal monsters.
// TODO: BUG: When game over, focused room is still focused.

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
