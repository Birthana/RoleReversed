using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public MonsterCard monsterCardPrefab;
    public RoomCard roomCardPrefab;
    [SerializeField] private List<CardInfo> cardInfos = new List<CardInfo>();

    public void Add(CardInfo cardInfo)
    {
        cardInfos.Add(cardInfo);
        GetComponent<DeckCount>().AddToDeck();
    }

    public Card Draw()
    {
        if (cardInfos.Count == 0)
        {
            return null;
        }

        var newCard = CreateCardWith(GetTopCard());
        RemoveTopCard();
        GetComponent<DeckCount>().DrawFromDeck();
        return newCard;
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
