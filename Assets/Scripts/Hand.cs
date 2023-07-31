using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public List<Card> hand = new List<Card>();
    public float SPACING;

    private void Start()
    {
        DisplayHand();
    }

    public void AddNewCard(Card card)
    {
        var newCard = Instantiate(card, transform);
        Add(newCard);
    }

    public void Add(Card card)
    {
        if (hand.Count == 8)
        {
            return;
        }

        hand.Add(card);
        DisplayHand();
    }

    public void Remove(Card card)
    {
        hand.Remove(card);
        DisplayHand();
    }

    public void DisplayHand()
    {
        var centerPosition = new CenterPosition(Vector3.zero, hand.Count, SPACING);
        for (int i = 0; i < hand.Count; i++)
        {
            hand[i].transform.localPosition = centerPosition.GetHorizontalOffsetPositionAt(i);
        }
    }
}
