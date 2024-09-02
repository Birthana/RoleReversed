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
    private bool pickedDraftCard = false;

    public void SetMouseWrapper(IMouseWrapper wrapper) { mouse = wrapper; }

    private void Awake()
    {
        SetMouseWrapper(new MouseWrapper());
        draftManager = GetComponent<DraftManager>();
        reRollButton = GetComponentInChildren<RerollButton>();
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
        if (PlayerClicksOnDraftCard() && !pickedDraftCard)
        {
            FindObjectOfType<ToolTipManager>().Clear();
            AddDraftCardToDeck();
        }

        if (!mouse.PlayerPressesLeftClick() && mouse.IsOnDraft() && IsOpen())
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
        SetSprite(closeShop);
    }

    public void EnableDraft() { pickedDraftCard = false; }

    private bool PlayerClicksOnDraftCard() { return mouse.PlayerPressesLeftClick() && mouse.IsOnDraft(); }

    private void AddDraftCardToDeck()
    {
        var draftCard = mouse.GetHitComponent<DraftCard>();
        draftManager.AddDraftCardToDeck(draftCard);
        pickedDraftCard = true;
        if (draftManager.IsEmpty())
        {
            draftManager.Draft(draftCardTransform);
        }
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
    }

    public void OpenShop()
    {
        FindObjectOfType<ToolTipManager>().Clear();
        Show();

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
        if (playerSouls.GetCurrentSouls() == 0)
        {
            return;
        }

        draftManager.Reroll(draftCardTransform);
        playerSouls.DecreaseSouls();
    }
}
