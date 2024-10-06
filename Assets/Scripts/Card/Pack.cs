using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pack : MonoBehaviour
{
    public int numberOfCards = 5;
    public LootAnimation lootAnimation;
    private event Action OnOpenPack;
    private List<CardInfo> cardInfos = new List<CardInfo>();
    private List<Card> cards = new List<Card>();
    private IMouseWrapper mouseWrapper;
    private bool openingPack = false;

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
        LoadCardsInPack(GetStarterPack());
        SetDrawOnOpen();
        SetDrawOnOpen();
        SetDrawOnOpen();
        SetDrawOnOpen();
        SetDrawOnOpen();
    }

    private void LoadCardsInPack(List<CardInfo> cards)
    {
        foreach (var cardInfo in cards)
        {
            cardInfos.Add(cardInfo);
        }
    }

    private List<CardInfo> GetStarterPack()
    {
        var rngCards = FindObjectOfType<CardManager>();
        return rngCards.GetValidStarterCardInfos(TotalCostIsGreaterThanEight);
    }

    public void LoadBasicPack()
    {
        LoadRandomMonster();
        LoadRandomRoom();
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
        GetComponent<Animator>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = null;
        AnimatePackCards();
        AddPackCardsToDeck();
    }

    private void AnimatePackCards()
    {
        var centerPosition = new CenterPosition(Vector2.zero, cardInfos.Count, 25);
        for (int index = 0; index < cardInfos.Count; index++)
        {
            var position = centerPosition.GetHorizontalOffsetPositionAt(index);
            AnimateCardAtPosition(cardInfos[index], position, LootAnimation.SHOW_TIME * index);
        }
    }

    private void AnimateCardAtPosition(CardInfo cardInfo, Vector3 position, float delay)
    {
        var animation = Instantiate(lootAnimation);
        animation.transform.position = position;
        animation.SetDelay(delay);
        var cardUI = Instantiate(cardInfo.GetCardUI(), animation.transform);
        cardUI.SetCardInfo(cardInfo);
        animation.AnimateLoot();
    }

    private void AddPackCardsToDeck()
    {
        var deck = FindObjectOfType<Deck>();
        for (int index = 0; index < cardInfos.Count; index++)
        {
            deck.Add(cardInfos[index]);
        }
    }

    public void LoadRandomMonster()
    {
        var rngCards = FindObjectOfType<CardManager>();
        cardInfos.Add(rngCards.GetValidCardInfo(rngCards.CardInfoIsMonster));
    }

    public void LoadRandomMonsterThatCostOne()
    {
        var rngCards = FindObjectOfType<CardManager>();
        cardInfos.Add(rngCards.GetValidCardInfo(rngCards.CardInfoIsMonsterAndCostOne));
    }

    public void LoadRandomMonsterThatCostTwo()
    {
        var rngCards = FindObjectOfType<CardManager>();
        cardInfos.Add(rngCards.GetValidCardInfo(rngCards.CardInfoIsMonsterAndCostTwo));
    }

    public void LoadRandomRoom()
    {
        var rngCards = FindObjectOfType<CardManager>();
        cardInfos.Add(rngCards.GetValidCardInfo(rngCards.CardInfoIsRoom));
    }

    public void LoadRandomConstructionRoom()
    {
        var rngCards = FindObjectOfType<CardManager>();
        cardInfos.Add(rngCards.GetValidCardInfo(rngCards.CardInfoIsConstructionRoom));
    }

    public void LoadRandomMediumCard()
    {
        var rngCards = FindObjectOfType<CardManager>();
        rngCards.UnlockMediumCards();
        cardInfos.Add(rngCards.GetValidCardInfo(rngCards.CardInfoIsMedium));
    }

    public void LoadRandomHardCard()
    {
        var rngCards = FindObjectOfType<CardManager>();
        rngCards.UnlockHardCards();
        cardInfos.Add(rngCards.GetValidCardInfo(rngCards.CardInfoIsHard));
    }

    private bool TotalCostIsGreaterThanEight(List<CardInfo> cardInfos)
    {
        return GetTotalCost(cardInfos) > 8;
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
            if (PackIsNotClicked() || openingPack)
            {
                return;
            }

            StartCoroutine(OpenPack());
        }
    }

    private IEnumerator OpenPack()
    {
        openingPack = true;
        CreatePackToHand();
        yield return new WaitForSeconds(LootAnimation.ANIMATION_TIME);
        OnOpenPack?.Invoke();
        DestroyImmediate(gameObject);
    }

    public void SetDrawOnOpen() { OnOpenPack += DrawCard; }
    public void SetGainActionOnOpen() { OnOpenPack += GainAction; }

    private void GainAction()
    {
        FindObjectOfType<ActionManager>().AddActions(1);
        OnOpenPack -= GainAction;
    }

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

    private bool PlayerClicksOnAPack() { return mouseWrapper.PlayerPressesLeftClick() && mouseWrapper.IsOnPack(); }
}
