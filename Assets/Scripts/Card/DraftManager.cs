using System.Collections.Generic;
using UnityEngine;

public class DraftManager : MonoBehaviour
{
    public float SPACING = 10;
    private List<DraftCard> draftCards = new List<DraftCard>();
    private int NUMBER_OF_DRAFT_CARDS = 3;
    private static readonly string DRAFT_CARD_FILE_PATH = "Prefabs/DraftCard";
    private DraftCard draftCardPrefab;
    private IMouseWrapper mouseWrapper;

    public void Awake()
    {
        draftCardPrefab = Resources.Load<DraftCard>(DRAFT_CARD_FILE_PATH);
        SetMouseWrapper(new MouseWrapper());
    }

    public void SetMouseWrapper(IMouseWrapper wrapper)
    {
        mouseWrapper = wrapper;
    }

    private bool PlayerClicksOnDraftCard() { return mouseWrapper.PlayerPressesLeftClick() && mouseWrapper.IsOnDraft(); }

    public void Update()
    {
        if (PlayerClicksOnDraftCard())
        {
            AddDraftCardToDeck();
            ClearDraftCards();
        }
    }

    private void AddDraftCardToDeck()
    {
        var chosenCard = mouseWrapper.GetHitComponent<DraftCard>();
        FindObjectOfType<Deck>().Add(chosenCard.GetCardInfo());
        FindObjectOfType<Deck>().DrawCardToHand();
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
    }

    private void CreateDraftCard(CardInfo cardInfo)
    {
        var newDraftCard = Instantiate(draftCardPrefab, transform);
        newDraftCard.SetCardInfo(cardInfo);
        draftCards.Add(newDraftCard);
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
