using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drop : DisplayObject
{
    public Sprite openBox;
    public Sprite closedBox;
    public Image boxSprite;
    public Image frontDeckBox;
    public Canvas cardUIPosition;
    public RectTransform dropCardPosition;
    private CardUI cardUI;

    protected override bool PlayerClicksOnObject() { return mouse.PlayerPressesLeftClick() && mouse.IsOnDrop(); }

    public int GetSize() { return cardInfos.Count; }

    private void RemoveTopOfDropCard()
    {
        if (cardUI != null)
        {
            DestroyImmediate(cardUI.gameObject);
        }
    }

    private void CreateTopOfDeckCard(CardInfo cardInfo)
    {
        RemoveTopOfDropCard();
        cardUI = Instantiate(cardInfo.GetDropCardUI(), cardUIPosition.GetComponent<RectTransform>());
        cardUI.GetComponent<RectTransform>().anchoredPosition = dropCardPosition.anchoredPosition;
        cardUI.SetCardInfo(cardInfo);
    }

    public void Add(CardInfo cardInfo)
    {
        if (GetSize() == 0)
        {
            boxSprite.sprite = openBox;
            frontDeckBox.gameObject.SetActive(true);
        }

        cardInfos.Add(cardInfo);
        CreateTopOfDeckCard(cardInfo);
        FindObjectOfType<DropCount>().AddToDrop();
    }

    public void ReturnCardsToDeck()
    {
        var deck = FindObjectOfType<Deck>();
        var dropCount = FindObjectOfType<DropCount>();
        foreach (var cardInfo in cardInfos)
        {
            deck.Add(cardInfo);
            dropCount.RemoveFromDrop();
        }

        cardInfos = new List<CardInfo>();
        boxSprite.sprite = closedBox;
        frontDeckBox.gameObject.SetActive(false);
        RemoveTopOfDropCard();
    }
}
