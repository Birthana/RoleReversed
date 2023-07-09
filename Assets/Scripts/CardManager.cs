using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public List<Card> cards = new List<Card>();

    public Card GetRandomCard()
    {
        int rngIndex = Random.Range(0, cards.Count);
        return cards[rngIndex];
    }

    public Card GetMonsterCard()
    {
        Card card = null;
        do
        {
            var rngCard = GetRandomCard();
            if(rngCard is MonsterCard && (rngCard.GetCost() < 3))
            {
                card = rngCard;
            }
        } while (card == null);

        return card;
    }

    public Card GetRoomCard()
    {
        Card card = null;
        do
        {
            var rngCard = GetRandomCard();
            if (rngCard is RoomCard && (rngCard.GetCost() < 3))
            {
                card = rngCard;
            }
        } while (card == null);

        return card;
    }
}
