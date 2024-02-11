using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : DisplayObject
{
    public MonsterCard monsterCardPrefab;
    public RoomCard roomCardPrefab;

    protected override bool PlayerClicksOnObject() { return mouse.PlayerPressesLeftClick() && mouse.IsOnDeck(); }

    public void Add(CardInfo cardInfo)
    {
        cardInfos.Add(cardInfo);
        GetComponent<DeckCount>().AddToDeck();
    }

    private bool IsEmpty() { return cardInfos.Count == 0; }

    public Card Draw()
    {
        if (IsEmpty())
        {
            ShuffleDropToDeck();

            if (IsEmpty())
            {
                return null;
            }
        }

        var newCard = CreateCardWith(GetTopCard());
        RemoveTopCard();
        GetComponent<DeckCount>().DrawFromDeck();
        return newCard;
    }

    private void ShuffleDropToDeck()
    {
        FindObjectOfType<Drop>().ReturnCardsToDeck();
        Shuffle();
    }

    private void Shuffle()
    {
        for (int index = 0; index < cardInfos.Count - 1; index++)
        {
            int randomIndex = index + Random.Range(0, cardInfos.Count - index);
            CardInfo randomCard = cardInfos[randomIndex];
            cardInfos[randomIndex] = cardInfos[index];
            cardInfos[index] = randomCard;
        }
    }

    private Card CreateCardWith(CardInfo cardInfo)
    {
        var newCard = CreateNewCard(cardInfo);
        newCard.SetCardInfo(cardInfo);
        return newCard;
    }

    private CardInfo GetTopCard() { return cardInfos[0]; }

    private void RemoveTopCard() { cardInfos.RemoveAt(0); }

    public void DrawCardToHand()
    {
        var hand = FindObjectOfType<Hand>();
        if (hand.IsFull())
        {
            return;
        }

        var card = Draw();
        if (card != null)
        {
            hand.Add(card);
        }
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


    public int GetSize() { return cardInfos.Count; }
}
