using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : DisplayObject
{
    public Sprite openBox;
    public Sprite closedBox;
    public SpriteRenderer frontDeckBox;
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
        cardUI = Instantiate(cardInfo.GetCardUI(), transform);
        cardUI.transform.localScale = new Vector3(0.65f, 0.65f, 1.0f);
        cardUI.SetCardInfo(cardInfo);
    }

    public void Add(CardInfo cardInfo)
    {
        if (GetSize() == 0)
        {
            GetComponent<SpriteRenderer>().sprite = openBox;
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
        GetComponent<SpriteRenderer>().sprite = closedBox;
        frontDeckBox.gameObject.SetActive(false);
        RemoveTopOfDropCard();
    }
}
