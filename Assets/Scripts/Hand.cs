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

    public void Add(Card card)
    {
        var newCard = Instantiate(card, transform);
        hand.Add(newCard);
        DisplayHand();
    }

    public void Remove(Card card)
    {
        hand.Remove(card);
        DisplayHand();
    }

    private void DisplayHand()
    {
        for (int i = 0; i < hand.Count; i++)
        {
            hand[i].transform.localPosition = CalcPositionAt(i);
        }
    }

    private Vector3 CalcPositionAt(int cardIndex)
    {
        float positionOffset = CalcPositionOffsetAt(cardIndex);
        return new Vector3(CalcX(positionOffset), CalcY(), CalcZ());
    }

    float CalcPositionOffsetAt(int index) { return index - ((float)hand.Count - 1) / 2; }

    float CalcX(float positionOffset) { return Mathf.Sin(positionOffset * Mathf.Deg2Rad) * SPACING * 10; }
    
    float CalcY() { return 0; }

    float CalcZ() { return 0; }

}
