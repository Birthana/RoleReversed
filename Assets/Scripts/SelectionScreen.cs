using System;
using System.Collections.Generic;
using UnityEngine;

public class SelectionScreen : MonoBehaviour
{
    public event Action OnSelectPressed;
    public event Action OnCancelPressed;
    public float SPACING;
    private List<Card> cards = new List<Card>();
    private int maxSelection;

    // Update is called once per frame
    void Update()
    {
        if (Mouse.PlayerPressesLeftClick() && Mouse.IsOnSelect())
        {
            OnSelectPressed?.Invoke();
        }

        if (Mouse.PlayerPressesLeftClick() && Mouse.IsOnCancel())
        {
            OnCancelPressed?.Invoke();
        }
    }

    public int GetSelections() { return cards.Count; }

    public void SetMaxSelection(int numberOfSelections) { maxSelection = numberOfSelections; }

    public void SetSelectButton(Action action) { OnSelectPressed += action; }
    public void RemoveSelectButton(Action action) { OnSelectPressed -= action; }

    public void SetCancelButton(Action action) { OnCancelPressed += action; }
    public void RemoveCancelButton(Action action) { OnCancelPressed -= action; }

    public bool IsFull() { return maxSelection == cards.Count; }

    public void AddToSelection(Card card)
    {
        if (IsFull())
        {
            return;
        }

        cards.Add(card);
        Display();
    }

    public void DestroyAllSelections()
    {
        foreach (var card in cards)
        {
            FindObjectOfType<Hand>().Remove(card);
            Destroy(card.gameObject);
        }

        cards = new List<Card>();
    }

    public void ReturnAllSelections()
    {
        FindObjectOfType<Hand>().DisplayHand();
        cards = new List<Card>();
    }

    private void Display()
    {
        var centerPosition = new CenterPosition(transform.position, cards.Count, SPACING);
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].transform.position = centerPosition.GetHorizontalOffsetPositionAt(i);
        }
    }
}
