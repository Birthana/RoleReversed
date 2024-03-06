using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraftManager : MonoBehaviour
{
    public float SPACING = 10;
    public LootAnimation lootAnimation;
    private List<DraftCard> draftCards = new List<DraftCard>();
    private int NUMBER_OF_DRAFT_CARDS = 3;
    private static readonly string DRAFT_CARD_FILE_PATH = "Prefabs/DraftCard";
    private DraftCard draftCardPrefab;
    private IMouseWrapper mouseWrapper;
    private bool isRunning;

    public void Awake()
    {
        draftCardPrefab = Resources.Load<DraftCard>(DRAFT_CARD_FILE_PATH);
        SetMouseWrapper(new MouseWrapper());
    }

    public void SetMouseWrapper(IMouseWrapper wrapper)
    {
        mouseWrapper = wrapper;
    }

    public bool IsRunning() { return isRunning; }

    private bool PlayerClicksOnDraftCard() { return mouseWrapper.PlayerPressesLeftClick() && mouseWrapper.IsOnDraft(); }

    public void Update()
    {
        if (PlayerClicksOnDraftCard())
        {
            AddDraftCardToDeck();
            ClearDraftCards();
        }

        if (mouseWrapper.PlayerPressesLeftClick() && !mouseWrapper.IsOnDraft())
        {
            foreach (var draftCard in draftCards)
            {
                draftCard.gameObject.SetActive(!draftCard.gameObject.activeInHierarchy);
            }
        }
    }

    private void AddDraftCardToDeck()
    {
        var deck = FindObjectOfType<Deck>();
        StartCoroutine(AnimateChosenCard(deck));
    }

    private IEnumerator AnimateChosenCard(Deck deck)
    {
        var chosenCard = mouseWrapper.GetHitComponent<DraftCard>();
        deck.Add(chosenCard.GetCardInfo());
        AnimateCard(chosenCard.GetCardInfo());
        yield return new WaitForSeconds(LootAnimation.ANIMATION_TIME);
        deck.DrawCardToHand();
        isRunning = false;
    }

    private void AnimateCard(CardInfo cardInfo)
    {
        var animation = Instantiate(lootAnimation);
        CreateCardUI(animation.transform, cardInfo);
        animation.AnimateLoot();
    }

    private void ClearDraftCards()
    {
        foreach (var card in FindObjectsOfType<DraftCard>())
        {
            DestroyImmediate(card.gameObject);
        }

        draftCards = new List<DraftCard>();
    }

    public int GetCount() { return draftCards.Count; }

    public void Draft()
    {
        var cardInfos = FindObjectOfType<CardManager>().GetUniqueCardInfos(NUMBER_OF_DRAFT_CARDS);
        for (int i = 0; i < cardInfos.Count; i++)
        {
            CreateDraftCard(cardInfos[i]);
        }

        DisplayDraftCards();
        isRunning = true;
    }

    private void CreateDraftCard(CardInfo cardInfo)
    {
        var newDraftCard = Instantiate(draftCardPrefab, transform);
        newDraftCard.SetCardInfo(cardInfo);
        CreateCardUI(newDraftCard.transform, cardInfo);
        draftCards.Add(newDraftCard);
    }

    private void CreateCardUI(Transform parent, CardInfo cardInfo)
    {
        var cardUI = Instantiate(cardInfo.GetCardUI(), parent);
        cardUI.SetCardInfo(cardInfo);
    }

    private void DisplayDraftCards()
    {
        var centerPosition = new CenterPosition(Vector3.zero, draftCards.Count, SPACING);
        for (int i = 0; i < draftCards.Count; i++)
        {
            draftCards[i].transform.localPosition = centerPosition.GetHorizontalOffsetPositionAt(i);
        }
    }
}
