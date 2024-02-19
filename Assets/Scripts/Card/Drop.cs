using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : DisplayObject
{
    public Sprite openBox;
    public Sprite closedBox;
    public SpriteRenderer frontDeckBox;
    [SerializeField] private DisplayCard displayCard;

    public void SetDisplayCard(DisplayCard displayCard)
    {
        this.displayCard = displayCard;
    }

    protected override bool PlayerClicksOnObject() { return mouse.PlayerPressesLeftClick() && mouse.IsOnDrop(); }

    public int GetSize() { return cardInfos.Count; }

    public void Add(CardInfo cardInfo)
    {
        if (GetSize() == 0)
        {
            GetComponent<SpriteRenderer>().sprite = openBox;
            frontDeckBox.gameObject.SetActive(true);
        }

        cardInfos.Add(cardInfo);
        displayCard.gameObject.SetActive(true);
        displayCard.SetCardInfo(cardInfo);
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
        displayCard.gameObject.SetActive(false);
    }
}
