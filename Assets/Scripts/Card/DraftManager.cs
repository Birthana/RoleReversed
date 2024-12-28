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
    [SerializeField] private List<DraftCard> draftCards = new List<DraftCard>();
    private IMouseWrapper mouse;
    private SoulShop soulShop;
    private bool isDrafting = false;

    public void SetMouseWrapper(IMouseWrapper wrapper) { mouse = wrapper; }


    public void Awake()
    {
        SetMouseWrapper(new MouseWrapper());
        draftCardPrefab = Resources.Load<DraftCard>(DRAFT_CARD_FILE_PATH);
        soulShop = FindObjectOfType<SoulShop>(true);
    }

    private void Update()
    {
        if (mouse.PlayerPressesLeftClick() && mouse.IsOnDraft() && isDrafting && !soulShop.IsOpen())
        {
            FindObjectOfType<ToolTipManager>().Clear();
            var card = mouse.GetHitComponent<DraftCard>();
            if (card.gameObject.transform.parent != transform)
            {
                return;
            }

            AddDraftCardToHand(card);
            foreach (var draftCard in draftCards)
            {
                AddDraftCardToDeck(draftCard);
                Destroy(draftCard.gameObject);
            }

            draftCards = new List<DraftCard>();
            isDrafting = false;
        }

        if (!mouse.PlayerPressesLeftClick() && mouse.IsOnDraft() && isDrafting && !soulShop.IsOpen())
        {
            var draftCard = mouse.GetHitComponent<DraftCard>();
            var position = draftCard.gameObject.transform.position + (Vector3.up * 5.0f);
            FindObjectOfType<ToolTipManager>().Set(draftCard.GetCardInfo(), position);
        }
    }

    public bool IsEmpty() { return draftCards.Count == 0; }

    public int GetCount() { return draftCards.Count; }

    public void AddDraftCardToDeck(DraftCard draftCard)
    {
        var deck = FindObjectOfType<Deck>();
        deck.Add(draftCard.GetCardInfo());
        StartCoroutine(AnimateChosenCard(draftCard));
    }

    public void AddDraftCardToHand(DraftCard draftCard)
    {
        var hand = FindObjectOfType<Hand>();
        if (hand.IsFull())
        {
            AddDraftCardToDeck(draftCard);
            return;
        }

        draftCards.Remove(draftCard);
        var deck = FindObjectOfType<Deck>();
        var newCard = deck.CreateCardWith(draftCard.GetCardInfo());
        hand.Add(newCard);
        FindObjectOfType<GlobalEffects>().DraftExit(newCard);
        StartCoroutine(AnimateChosenCard(draftCard));
    }

    private IEnumerator AnimateChosenCard(DraftCard draftCard)
    {
        AnimateCard(draftCard.GetCardInfo());
        Destroy(draftCard.gameObject);
        yield return new WaitForSeconds(LootAnimation.ANIMATION_TIME);
    }

    private void AnimateCard(CardInfo cardInfo)
    {
        var animation = Instantiate(lootAnimation);
        CreateCardUI(animation.transform, cardInfo);
        animation.AnimateLoot();
    }

    public void Reroll(Transform draftTransform)
    {
        foreach (var draftCard in draftCards)
        {
            Destroy(draftCard.gameObject);
        }

        draftCards = new List<DraftCard>();
        Draft(draftTransform);
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

    public void Draft(int numberOfCards, Tag tag)
    {
        var cardInfos = FindObjectOfType<CardManager>().GetUniqueCardInfos(numberOfCards, tag);
        for (int i = 0; i < cardInfos.Count; i++)
        {
            CreateDraftCard(cardInfos[i], transform);
        }

        DisplayDraftCards(transform);
        isDrafting = true;
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
