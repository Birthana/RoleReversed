using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleDeck : MonoBehaviour
{
    private event Action<BattleCardInfo> OnCardPlayed;
    private event Action<BattleCardInfo> OnCardAdded;
    private event Action OnShuffle;
    private event Action<float> OnBattleDeckWait;
    private SpriteRenderer deckSprite;
    [SerializeField]private List<BattleCardInfo> deck = new List<BattleCardInfo>();

    private void Awake()
    {
        deckSprite = GetComponentInChildren<SpriteRenderer>(true);
        deckSprite.gameObject.SetActive(false);
    }

    public void AddToOnCardPlayed(Action<BattleCardInfo> func) { OnCardPlayed += func; }

    public void AddToOnCardAdded(Action<BattleCardInfo> func) { OnCardAdded += func; }

    public void AddToOnShuffle(Action func) { OnShuffle += func; }

    public void AddToOnBattleDeckWait(Action<float> func) { OnBattleDeckWait += func; }

    public bool IsEmpty() { return deck.Count == 0; }

    public void Clear()
    {
        deckSprite.gameObject.SetActive(false);
        deck = new List<BattleCardInfo>();
    }

    public void Add(BattleCardInfo card)
    {
        OnCardAdded?.Invoke(card);
        deckSprite.gameObject.SetActive(true);
        deck.Add(card);
    }

    public void Remove(Character character)
    {
        foreach (var card in deck.ToList())
        {
            if (card.GetCharacter().Equals(character))
            {
                deck.Remove(card);
            }
        }
    }

    public void Shuffle()
    {
        if (deck.Count == 0)
        {
            return;
        }

        OnShuffle?.Invoke();
        for (int index = 0; index < deck.Count - 1; index++)
        {
            int randomIndex = index + UnityEngine.Random.Range(0, deck.Count - index);
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
        if (!topCard.GetCharacter().IsDead())
        {
            OnCardPlayed?.Invoke(topCard);
        }

        yield return topCard.Play();

        if (!topCard.GetCharacter().IsDead())
        {
            OnBattleDeckWait?.Invoke(GameManager.ATTACK_TIMER / 2);
        }
    }
}
