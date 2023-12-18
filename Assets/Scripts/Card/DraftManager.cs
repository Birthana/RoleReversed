using System.Collections.Generic;
using UnityEngine;

public class DraftManager : MonoBehaviour
{
    public float SPACING = 10;
    private List<DraftCard> draftCards = new List<DraftCard>();
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

    public void Update()
    {
        if (mouseWrapper.PlayerPressesLeftClick() && mouseWrapper.IsOnDraft())
        {
            var draftCard = mouseWrapper.GetHitComponent<DraftCard>();
            var cardInfo = draftCard.GetCardInfo();
            var newCard = FindObjectOfType<CardManager>().CreateCard(cardInfo);
            FindObjectOfType<Hand>().Add(newCard);
            var allDraftCards = FindObjectsOfType<DraftCard>();
            foreach (var card in allDraftCards)
            {
                DestroyImmediate(card.gameObject);
            }
            draftCards = new List<DraftCard>();
        }
    }

    private bool CardInfoIsNotUnique(CardInfo cardInfo)
    {
        if (draftCards.Count == 0)
        {
            return false;
        }

        foreach (var draftCard in draftCards)
        {
            if (draftCard.GetCardInfo().Equals(cardInfo))
            {
                return true;
            }
        }

        return false;
    }

    public int GetCount() { return draftCards.Count; }

    public void Draft()
    {
        var cardManager = FindObjectOfType<CardManager>();
        for(int i = 0; i < 3; i++)
        {
            CardInfo cardInfo;
            do
            {
                cardInfo = cardManager.GetValidCard(CardInfoIsValid);
            } while (CardInfoIsNotUnique(cardInfo));
            var newDraftCard = Instantiate(draftCardPrefab, transform);
            newDraftCard.SetCardInfo(cardInfo);
            draftCards.Add(newDraftCard);
        }

        DisplayDraftCards();
    }

    private void DisplayDraftCards()
    {
        var centerPosition = new CenterPosition(Vector3.zero, draftCards.Count, SPACING);
        for (int i = 0; i < draftCards.Count; i++)
        {
            draftCards[i].transform.localPosition = centerPosition.GetHorizontalOffsetPositionAt(i);
        }
    }

    private bool CardInfoIsValid(CardInfo cardInfo)
    {
        return true;
    }
}
