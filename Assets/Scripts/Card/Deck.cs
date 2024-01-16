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
    }

    public Card Draw()
    {
        if(cardInfos.Count == 0)
        {
            return null;
        }

        var cardInfo = cardInfos[0];
        cardInfos.RemoveAt(0);
        var newCard = CreateNewCard(cardInfo);
        newCard.SetCardInfo(cardInfo);
        return newCard;
    }

    public void DrawCardToHand()
    {
        var card = Draw();
        if (card != null)
        {
            FindObjectOfType<Hand>().Add(card);
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
