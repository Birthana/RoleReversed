using UnityEngine;


public class SoulShop : MonoBehaviour
{
    public Sprite openShop;
    public Sprite closeShop;
    [SerializeField] private GameObject background;
    [SerializeField] private Transform draftCardTransform;
    private DraftManager draftManager;
    private RerollButton reRollButton;
    private PlayerSoulCounter playerSouls;
    private IMouseWrapper mouse;

    public void SetMouseWrapper(IMouseWrapper wrapper) { mouse = wrapper; }

    private void Awake()
    {
        SetMouseWrapper(new MouseWrapper());
        draftManager = GetComponent<DraftManager>();
        reRollButton = GetComponentInChildren<RerollButton>(true);
        playerSouls = FindObjectOfType<PlayerSoulCounter>();
        Hide();
    }

    private void Show()
    {
        background.SetActive(true);
        draftCardTransform.gameObject.SetActive(true);
        reRollButton.gameObject.SetActive(true);
    }

    private void Hide()
    {
        background.SetActive(false);
        draftCardTransform.gameObject.SetActive(false);
        reRollButton.gameObject.SetActive(false);
    }

    public void Update()
    {
        if (PlayerChoosesDraftCard())
        {
            FindObjectOfType<ToolTipManager>().Clear();
            playerSouls.DecreaseSouls(1);
            var draftCard = mouse.GetHitComponent<DraftCard>();
            if (draftCard.gameObject.transform.parent != draftCardTransform)
            {
                return;
            }

            AddDraftCardToDeck(draftCard);
        }

        if (PlayerHoversDraftCard())
        {
            var draftCard = mouse.GetHitComponent<DraftCard>();
            var position = draftCard.gameObject.transform.position + (Vector3.up * 5.0f);
            FindObjectOfType<ToolTipManager>().Set(draftCard.GetCardInfo(), position);
        }

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
    }

    private bool PlayerChoosesDraftCard()
    {
        return PlayerClicksOnDraftCard() && (playerSouls.GetCurrentSouls() > 0) && IsOpen();
    }

    private bool PlayerHoversDraftCard() { return !mouse.PlayerPressesLeftClick() && mouse.IsOnDraft() && IsOpen(); }

    private bool PlayerClicksOnDraftCard() { return mouse.PlayerPressesLeftClick() && mouse.IsOnDraft(); }

    private void AddDraftCardToDeck(DraftCard card)
    {
        draftManager.AddDraftCardToHand(card);
        if (draftManager.IsEmpty())
        {
            draftManager.Draft(draftCardTransform);
        }

        CloseShop();
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
        Hide();
        SetSprite(openShop);
        FindObjectOfType<HealthBar>().Show();
    }

    public void OpenShop()
    {
        FindObjectOfType<ToolTipManager>().Clear();
        Show();
        SetSprite(closeShop);
        FindObjectOfType<HealthBar>().Hide();

        if (!ShopIsEmpty())
        {
            return;
        }

        draftManager.Draft(draftCardTransform);
    }

    private bool ShopIsEmpty() { return draftManager.IsEmpty(); }

    public bool IsOpen()
    {
        if (ShopIsEmpty())
        {
            return false;
        }

        return draftCardTransform.gameObject.activeSelf;
    }

    public void Reroll()
    {
        if (playerSouls.GetCurrentSouls() < reRollButton.GetCost())
        {
            return;
        }

        draftManager.Reroll(draftCardTransform);

        if (reRollButton.GetCost() == 0)
        {
            return;
        }

        playerSouls.DecreaseSouls();
    }

    public void FreeReroll()
    {
        draftManager.Reroll(draftCardTransform);
    }
}
