using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDeck : MonoBehaviour
{
    [SerializeField]private List<BattleCardInfo> deck = new List<BattleCardInfo>();
    private List<BattleCardInfo> drop = new List<BattleCardInfo>();

    public void Clear()
    {
        deck = new List<BattleCardInfo>();
        drop = new List<BattleCardInfo>();
    }

    public void Add(BattleCardInfo card)
    {
        deck.Add(card);
    }

    public void Shuffle()
    {
        if (deck.Count == 0)
        {
            return;
        }

        for (int index = 0; index < deck.Count - 1; index++)
        {
            int randomIndex = index + Random.Range(0, deck.Count - index);
            BattleCardInfo randomCard = deck[randomIndex];
            deck[randomIndex] = deck[index];
            deck[index] = randomCard;
        }
    }

    public IEnumerator PlayTopCard()
    {
        if (deck.Count == 0)
        {
            yield return null;
        }

        var topCard = deck[0];
        deck.RemoveAt(0);
        yield return topCard.Play();
        drop.Add(topCard);
    }
}
