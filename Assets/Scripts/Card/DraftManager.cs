using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DraftManager : MonoBehaviour
{
    [SerializeField] private Button showFieldButton;
    [SerializeField] private Sprite draftShow;
    [SerializeField] private Sprite draftHide;
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
        SetupShowFieldButton();
    }

    private void SetupShowFieldButton()
    {
        if (showFieldButton == null)
        {
            return;
        }

        showFieldButton.onClick.AddListener(ToggleDraftCards);
        SetShowFieldButtonState(false);
    }

    public void SetupShowFieldButton(Button button) { showFieldButton = button; }

    public Button GetShowFieldButton() { return showFieldButton; }

    public void SetMouseWrapper(IMouseWrapper wrapper)
    {
        mouseWrapper = wrapper;
    }

    public bool IsRunning() { return isRunning; }

    private bool PlayerClicksOnDraftCard() { return mouseWrapper.PlayerPressesLeftClick() && mouseWrapper.IsOnDraft(); }

    public void Update()
    {
        if (!mouseWrapper.PlayerPressesLeftClick() && mouseWrapper.IsOnDraft())
        {
            var draftCard = mouseWrapper.GetHitComponent<DraftCard>();
            var position = draftCard.gameObject.transform.position + (Vector3.up * 7.0f);
            FindObjectOfType<ToolTipManager>().SetText(draftCard.GetCardInfo().effectDescription, position);
        }

        if (PlayerClicksOnDraftCard())
        {
            SetShowFieldButtonState(false);
            AddDraftCardToDeck();
            ClearDraftCards();
        }
    }

    public void ToggleDraftCards()
    {
        foreach (var draftCard in draftCards)
        {
            draftCard.gameObject.SetActive(!draftCard.gameObject.activeInHierarchy);
        }

        ChangeButtonSprite(draftCards[0]);
    }

    private void ChangeButtonSprite(DraftCard draftCard)
    {
        if (draftCard.gameObject.activeInHierarchy)
        {
            ChangeShowFieldButtonSprite(draftShow);
            return;
        }

        ChangeShowFieldButtonSprite(draftHide);
    }

    private void ChangeShowFieldButtonSprite(Sprite sprite)
    {
        showFieldButton.GetComponent<Image>().sprite = sprite;
    }

    private void SetShowFieldButtonState(bool state)
    {
        showFieldButton.gameObject.SetActive(state);
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
        SetShowFieldButtonState(true);
        ChangeShowFieldButtonSprite(draftShow);
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
