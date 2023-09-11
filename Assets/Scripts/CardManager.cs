using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public List<Card> commons = new List<Card>();
    public List<Card> rares = new List<Card>();

    public Card GetRandomCard()
    {
        int rngIndex = Random.Range(0, 2);
        if (rngIndex == 0)
        {
            return GetCommonCard();
        }

        return GetRareCard();
    }

    public Card GetCommonCard()
    {
        int rngIndex = Random.Range(0, commons.Count);
        return commons[rngIndex];
    }

    public Card GetRareCard()
    {
        int rngIndex = Random.Range(0, rares.Count);
        return rares[rngIndex];
    }

    public bool CardIsCommon(Card card)
    {
        foreach(var common in commons)
        {
            if(card.Equals(common))
            {
                return true;
            }
        }

        return false;
    }

    public bool CardIsRare(Card card)
    {
        foreach (var rare in rares)
        {
            if (card.Equals(rare))
            {
                return true;
            }
        }

        return false;
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
