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
}
