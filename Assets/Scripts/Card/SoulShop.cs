using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulShop : MonoBehaviour
{
    public Sprite openShop;
    public Sprite closeShop;
    public float SPACING = 5.0f;
    [SerializeField] private GameObject background;
    [SerializeField] private Transform draftCardTransform;
    private IMouseWrapper mouse;
    private List<DraftCard> draftCards = new List<DraftCard>();
    private static readonly string DRAFT_CARD_FILE_PATH = "Prefabs/DraftCard";
    private DraftCard draftCardPrefab;

    public void SetMouseWrapper(IMouseWrapper wrapper) { mouse = wrapper; }

    private void Awake()
    {
        draftCardPrefab = Resources.Load<DraftCard>(DRAFT_CARD_FILE_PATH);
        SetMouseWrapper(new MouseWrapper());
        background.SetActive(false);
        draftCardTransform.gameObject.SetActive(false);
    }

    public void Update()
    {
        if (PlayerDoesNotClickOnShop())
        {
            return;
        }

        if (IsOpen())
        {
            CloseShop();
            return;
        }

        OpenShop();
        SetSprite(closeShop);
    }

    private void SetSprite(Sprite sprite)
    {
        var renderer = GetComponent<SpriteRenderer>();
        if (renderer == null)
        {
            return;
        }

        renderer.sprite = sprite;
    }

    private bool PlayerDoesNotClickOnShop() { return (!mouse.PlayerPressesLeftClick() || !mouse.IsOnOpenSoulShop()); }

    public void CloseShop()
    {
        background.SetActive(false);
        draftCardTransform.gameObject.SetActive(false);
        SetSprite(openShop);
    }

    public void OpenShop()
    {
        FindObjectOfType<ToolTipManager>().Clear();
        background.SetActive(true);
        draftCardTransform.gameObject.SetActive(true);

        if (!ShopIsEmpty())
        {
            return;
        }

        GetDraftCards();
    }

    private bool ShopIsEmpty() { return draftCards.Count == 0; }

    public bool IsOpen()
    {
        if (ShopIsEmpty())
        {
            return false;
        }

        return draftCardTransform.gameObject.activeSelf;
    }

    public void GetDraftCards()
    {
        var cardInfos = FindObjectOfType<CardManager>().GetUniqueCardInfos(5);
        for (int i = 0; i < cardInfos.Count; i++)
        {
            CreateDraftCard(cardInfos[i]);
        }

        DisplayDraftCards();
    }

    private void CreateDraftCard(CardInfo cardInfo)
    {
        var newDraftCard = Instantiate(draftCardPrefab, draftCardTransform);
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
        var centerPosition = new CenterPosition(draftCardTransform.position, draftCards.Count, SPACING);
        for (int i = 0; i < draftCards.Count; i++)
        {
            draftCards[i].transform.position = centerPosition.GetHorizontalOffsetPositionAt(i);
        }
    }
}
