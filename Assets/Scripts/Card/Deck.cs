using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deck : DisplayObject
{
    [SerializeField] private List<CardInfo> topOfDeck;
    public Sprite openBox;
    public Sprite closedBox;
    public Image deckSprite;
    public AudioClip draw;
    public AudioClip shuffle;
    [SerializeField] private DeckCount deckCount;
    private Drop drop;
    private Hand hand;
    private SoundManager soundManager;

    private DeckCount GetDeckCount() { return deckCount; }

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

    private SoundManager GetSoundManager()
    {
        if (soundManager == null)
        {
            soundManager = GetComponent<SoundManager>();
        }

        return soundManager;
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
            deckSprite.sprite = sprite;
        }
    }

    private bool IsEmpty() { return cardInfos.Count == 0; }

    private bool Contains(CardInfo cardInfo) { return cardInfos.Contains(cardInfo); }

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
        if (topOfDeck.Count != 0)
        {
            var copy = Instantiate(topOfDeck[0]);
            var topCard = CreateCardWith(copy);
            topOfDeck.RemoveAt(0);
            return topCard;
        }

        return Draw(GetTopCard());
    }

    private Card Draw(CardInfo cardInfo)
    {
        if (cardInfo == null)
        {
            return null;
        }

        GetSoundManager().Play(draw);
        var newCard = CreateCardWith(cardInfo);
        cardInfos.Remove(cardInfo);
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

        GetSoundManager().Play(shuffle);
    }

    public Card CreateCardWith(CardInfo cardInfo)
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

    public Card DrawCardToHand()
    {
        if (GetHand().IsFull())
        {
            return null;
        }

        var card = Draw();
        AddCardToHand(card);
        return card;
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
