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
        var blockCenterPosition = new BlockCenterPosition(transform.position,
                                                          displayCards.Count,
                                                          MAX_DISPLAY_COLUMN_COUNT,
                                                          25.0f,
                                                          ROW_SPACING);
        for (int i = 0; i < displayCards.Count; i++)
        {
            displayCards[i].transform.position = blockCenterPosition.GetHorizontalLayoutPositionAt(i);
        }
    }
}
