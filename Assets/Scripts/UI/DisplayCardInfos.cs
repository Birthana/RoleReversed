using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayCardInfos : MonoBehaviour
{
    public DisplayCard displayCardPrefab;
    private List<DisplayCard> displayCards = new List<DisplayCard>();
    private bool isDisplaying = false;
    private int MAX_DISPLAY_COLUMN_COUNT = 8;
    private float ROW_SPACING = 4.5f;

    public bool IsDisplay() { return isDisplaying; }

    public void Show(List<CardInfo> cardInfos)
    {
        isDisplaying = true;
        CreateAndDisplay(cardInfos);
    }

    private void CreateAndDisplay(List<CardInfo> cardInfos)
    {
        foreach (var cardInfo in cardInfos)
        {
            var displayCard = Instantiate(displayCardPrefab, transform);
            displayCard.Set(cardInfo);
            displayCards.Add(displayCard);
        }

        DisplayDisplayCard();
    }

    public void Hide()
    {
        isDisplaying = false;
        DestroyDisplayCards();
    }

    private void DestroyDisplayCards()
    {
        foreach (var card in displayCards)
        {
            Destroy(card.gameObject);
        }

        displayCards = new List<DisplayCard>();
    }

    private void DisplayDisplayCard()
    {
        var centerPostion = new CenterPosition(transform.position, GetDeckCardCount(), 25.0f);
        for (int index = 0; index < displayCards.Count; index++)
        {
            var position = centerPostion.GetHorizontalOffsetPositionAt(index % MAX_DISPLAY_COLUMN_COUNT);
            position = new Vector3(position.x, GetVerticalPosition(index), position.z);
            displayCards[index].transform.position = position;
        }
    }

    private float GetVerticalPosition(int index)
    {
        int row = index / MAX_DISPLAY_COLUMN_COUNT;
        return transform.position.x - (row * ROW_SPACING);
    }

    private int GetDeckCardCount()
    {
        if (displayCards.Count < MAX_DISPLAY_COLUMN_COUNT)
        {
            return displayCards.Count;
        }

        return MAX_DISPLAY_COLUMN_COUNT;
    }
}
