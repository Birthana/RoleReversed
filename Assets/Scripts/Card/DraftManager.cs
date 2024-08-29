using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraftManager : MonoBehaviour
{
    public float SPACING = 5;
    public LootAnimation lootAnimation;
    public int NUMBER_OF_DRAFT_CARDS = 5;
    private static readonly string DRAFT_CARD_FILE_PATH = "Prefabs/DraftCard";
    private DraftCard draftCardPrefab;
    private List<DraftCard> draftCards = new List<DraftCard>();

    public void Awake()
    {
        draftCardPrefab = Resources.Load<DraftCard>(DRAFT_CARD_FILE_PATH);
    }

    public bool IsEmpty() { return draftCards.Count == 0; }

    public int GetCount() { return draftCards.Count; }

    public void AddDraftCardToDeck(DraftCard draftCard)
    {
        var deck = FindObjectOfType<Deck>();
        StartCoroutine(AnimateChosenCard(deck, draftCard));
    }

    private IEnumerator AnimateChosenCard(Deck deck, DraftCard draftCard)
    {
        deck.Add(draftCard.GetCardInfo());
        AnimateCard(draftCard.GetCardInfo());
        draftCards.Remove(draftCard);
        Destroy(draftCard.gameObject);
        yield return new WaitForSeconds(LootAnimation.ANIMATION_TIME);
    }

    private void AnimateCard(CardInfo cardInfo)
    {
        var animation = Instantiate(lootAnimation);
        CreateCardUI(animation.transform, cardInfo);
        animation.AnimateLoot();
    }

    public void Draft(Transform draftTransform)
    {
        var cardInfos = FindObjectOfType<CardManager>().GetUniqueCardInfos(NUMBER_OF_DRAFT_CARDS);
        for (int i = 0; i < cardInfos.Count; i++)
        {
            CreateDraftCard(cardInfos[i], draftTransform);
        }

        DisplayDraftCards(draftTransform);
    }

    private void CreateDraftCard(CardInfo cardInfo, Transform draftTransform)
    {
        var newDraftCard = Instantiate(draftCardPrefab, draftTransform);
        newDraftCard.SetCardInfo(cardInfo);
        CreateCardUI(newDraftCard.transform, cardInfo);
        draftCards.Add(newDraftCard);
    }

    private void CreateCardUI(Transform parent, CardInfo cardInfo)
    {
        var cardUI = Instantiate(cardInfo.GetCardUI(), parent);
        cardUI.SetCardInfo(cardInfo);
    }

    private void DisplayDraftCards(Transform draftTransform)
    {
        var centerPosition = new CenterPosition(draftTransform.position, draftCards.Count, SPACING);
        for (int i = 0; i < draftCards.Count; i++)
        {
            draftCards[i].transform.position = centerPosition.GetHorizontalOffsetPositionAt(i);
        }
    }
}
