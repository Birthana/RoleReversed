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
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].transform.position = CalcPositionAt(i);
        }
    }

    private Vector3 CalcPositionAt(int cardIndex)
    {
        float positionOffset = CalcPositionOffsetAt(cardIndex);
        return new Vector3(CalcX(positionOffset), CalcY(), CalcZ());
    }

    float CalcPositionOffsetAt(int index) { return index - ((float)cards.Count - 1) / 2; }

    float CalcX(float positionOffset) { return Mathf.Sin(positionOffset * Mathf.Deg2Rad) * SPACING * 10; }

    float CalcY() { return 2.5f; }

    float CalcZ() { return 0; }

}
