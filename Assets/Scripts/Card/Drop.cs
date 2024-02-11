using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : DisplayObject
{
    protected override bool PlayerClicksOnObject() { return mouse.PlayerPressesLeftClick() && mouse.IsOnDrop(); }

    public int GetSize() { return cardInfos.Count; }

    public void Add(CardInfo cardInfo)
    {
        cardInfos.Add(cardInfo);
        SetSprite(cardInfo.cardSprite);
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
        SetSprite(null);
    }

    private void SetSprite(Sprite sprite) { GetComponent<SpriteRenderer>().sprite = sprite; }
}
