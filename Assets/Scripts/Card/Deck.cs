using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : DisplayObject
{
    public Sprite openBox;
    public Sprite closedBox;
    private SpriteRenderer spriteRenderer;
    private DeckCount deckCount;
    private Drop drop;
    private Hand hand;

    private SpriteRenderer GetSpriteRenderer()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        return spriteRenderer;
    }

    private DeckCount GetDeckCount()
    {
        if (deckCount == null)
        {
            deckCount = GetComponent<DeckCount>();
        }

        return deckCount;
    }

    private Drop GetDrop()
    {
        if (drop == null)
        {
            drop = FindObjectOfType<Drop>();
        }

        return drop;
    }

    private Hand GetHand()
    {
        if (hand == null)
        {
            hand = FindObjectOfType<Hand>();
        }

        return hand;
    }

    protected override bool PlayerClicksOnObject() { return mouse.PlayerPressesLeftClick() && mouse.IsOnDeck(); }

    public void Add(CardInfo cardInfo)
    {
        ChangeSprite(openBox);
        cardInfos.Add(cardInfo);
        GetDeckCount().AddToDeck();
    }

    private void ChangeSprite(Sprite sprite)
    {
        if (IsEmpty())
        {
            GetSpriteRenderer().sprite = sprite;
        }
    }

    private bool IsEmpty() { return cardInfos.Count == 0; }

    private bool Contains(CardInfo cardInfo) { return cardInfos.Contains(cardInfo); }

    private void Remove(CardInfo cardInfo)
    {
        cardInfos.Remove(cardInfo);
        Shuffle();
    }

    public void DrawSpecificCardToHand(CardInfo cardInfo)
    {
        if (GetHand().IsFull())
        {
            return;
        }

        if (!Contains(cardInfo))
        {
            return;
        }

        AddCardToHand(Draw(cardInfo));
    }

    public Card Draw()
    {
        return Draw(GetTopCard());
    }

    private Card Draw(CardInfo cardInfo)
    {
        if (cardInfo == null)
        {
            return null;
        }

        var newCard = CreateCardWith(cardInfo);
        Remove(cardInfo);
        GetDeckCount().DrawFromDeck();
        ChangeSprite(closedBox);
        return newCard;
    }

    private void ShuffleDropToDeck()
    {
        GetDrop().ReturnCardsToDeck();
        Shuffle();
    }

    private void Shuffle()
    {
        for (int index = 0; index < cardInfos.Count - 1; index++)
        {
            int randomIndex = index + Random.Range(0, cardInfos.Count - index);
            CardInfo randomCard = cardInfos[randomIndex];
            cardInfos[randomIndex] = cardInfos[index];
            cardInfos[index] = randomCard;
        }
    }

    private Card CreateCardWith(CardInfo cardInfo)
    {
        var newCard = CreateNewCard(cardInfo);
        newCard.SetCardInfo(cardInfo);
        return newCard;
    }

    private CardInfo GetTopCard()
    {
        if (IsEmpty())
        {
            ShuffleDropToDeck();

            if (IsEmpty())
            {
                return null;
            }
        }

        return cardInfos[0];
    }

    public void DrawCardToHand()
    {
        if (GetHand().IsFull())
        {
            return;
        }

        var card = Draw();
        AddCardToHand(card);
    }

    private void AddCardToHand(Card card)
    {
        if (card == null)
        {
            return;
        }

        GetHand().Add(card);
    }

    private Card CreateNewCard(CardInfo cardInfo) { return Instantiate(cardInfo.GetCardPrefab(), transform); }

    public int GetSize() { return cardInfos.Count; }
}
