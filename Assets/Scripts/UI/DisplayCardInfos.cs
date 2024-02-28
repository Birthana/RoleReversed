using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayCardInfos : MonoBehaviour
{
    private List<CardUI> cardUIs = new List<CardUI>();
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
            var cardUI = Instantiate(cardInfo.GetCardUI(), transform);
            cardUI.SetCardInfo(cardInfo);
            cardUIs.Add(cardUI);
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
        foreach (var card in cardUIs)
        {
            Destroy(card.gameObject);
        }

        cardUIs = new List<CardUI>();
    }

    private void DisplayDisplayCard()
    {
        var blockCenterPosition = new BlockCenterPosition(transform.position,
                                                          cardUIs.Count,
                                                          MAX_DISPLAY_COLUMN_COUNT,
                                                          25.0f,
                                                          ROW_SPACING);
        for (int i = 0; i < cardUIs.Count; i++)
        {
            cardUIs[i].transform.position = blockCenterPosition.GetHorizontalLayoutPositionAt(i);
        }
    }
}
