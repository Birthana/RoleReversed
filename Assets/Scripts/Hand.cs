using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public List<Card> hand = new List<Card>();
    public float SPACING;
    public int HANDSIZE;
    private ICardDragger cardDragger;

    public bool IsFull() { return hand.Count == HANDSIZE; }

    public void SetCardDragger(ICardDragger newCardDragger)
    {
        cardDragger = newCardDragger;
    }

    public int GetSize() { return hand.Count; }

    public MonsterCard RandomMonsterAttacks(EffectInput input)
    {
        var randomHandMonster = GetRandomMonsterCard();
        if (randomHandMonster == null)
        {
            return null;
        }

        randomHandMonster.MakeHandAttack(input);
        return randomHandMonster;
    }

    public void IncreaseStats()
    {
        foreach (var card in hand)
        {
            if (!(card.GetCardInfo() is MonsterCardInfo))
            {
                continue;
            }

            ((MonsterCard)card).IncreaseStats(1, 1);
        }
    }

    private MonsterCard GetRandomMonsterCard()
    {
        if (hand.Count == 0 || NoMonsters())
        {
            return null;
        }

        Card card;
        do
        {
            card = hand[Random.Range(0, hand.Count)];
        } while (!(card.GetCardInfo() is MonsterCardInfo));

        return (MonsterCard)card;
    }

    private bool NoMonsters()
    {
        foreach (var card in hand)
        {
            if (card.GetCardInfo() is MonsterCardInfo)
            {
                return false;
            }
        }

        return true;
    }


    public void Add(Card card)
    {
        if (IsFull())
        {
            return;
        }

        card.gameObject.transform.SetParent(transform);
        hand.Add(card);
        DisplayHand();
    }

    public void AddAndAttack(CardInfo cardInfo, EffectInput input)
    {
        var deck = FindObjectOfType<Deck>();
        var card = (MonsterCard)deck.CreateCardWith(cardInfo);
        Add(card);
        card.MakeHandAttack(input);
    }

    public void Remove(Card card)
    {
        for (var index = 0; index < hand.Count; index++)
        {
            if(card.gameObject.Equals(hand[index].gameObject))
            {
                hand.RemoveAt(index);
                break;
            }
        }

        DisplayHand();
    }

    public void DisplayHand()
    {
        var centerPosition = new CenterPosition(Vector3.zero, hand.Count, SPACING);
        for (int i = 0; i < hand.Count; i++)
        {
            hand[i].transform.localPosition = centerPosition.GetHorizontalOffsetPositionAt(i);
        }

        if (cardDragger == null)
        {
            SetCardDragger(FindObjectOfType<CardDragger>());
        }

        cardDragger.ResetHover();
    }
}
