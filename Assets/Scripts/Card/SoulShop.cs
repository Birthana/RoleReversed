using System.Collections.Generic;
using UnityEngine;


public class SoulShop : MonoBehaviour
{
    public Sprite openShop;
    public Sprite closeShop;
    [SerializeField] private GameObject background;
    [SerializeField] private Transform draftCardTransform;
    private DraftManager draftManager;
    private IMouseWrapper mouse;
    private bool pickedDraftCard = false;

    public void SetMouseWrapper(IMouseWrapper wrapper) { mouse = wrapper; }

    private void Awake()
    {
        SetMouseWrapper(new MouseWrapper());
        background.SetActive(false);
        draftManager = GetComponent<DraftManager>();
        draftCardTransform.gameObject.SetActive(false);
    }

    public void Update()
    {
        if (PlayerClicksOnDraftCard() && !pickedDraftCard)
        {
            FindObjectOfType<ToolTipManager>().Clear();
            AddDraftCardToDeck();
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
}
