using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolTipManager : MonoBehaviour
{
    private static readonly string TOOL_TIP_INFO_FILE_PATH = "Prefabs/Tooltips";
    private static readonly Vector3 EFFECT_TEXT_POSITION_OFFSET = Vector3.right * 5;
    private static readonly Vector3 EFFECT_TEXT_POSITION_OFFSET_MULTIPLE = Vector3.up * 5;
    private static readonly int NUMBER_OF_EXTRA_TOOL_TIPS_IN_COLUMN = 3;
    private static readonly float EXTRA_TOOL_TIP_SPACING = 27;
    public GameObject toolTipPrefab;
    public GameObject effectToolTipPrefab;
    private List<TooltipInfo> tooltips = new List<TooltipInfo>();
    private TextMeshPro toolTip;
    private List<TextMeshPro> effectTips = new List<TextMeshPro>();
    private bool isDisabled = false;
    [SerializeField] private CardInfo currentlyDisplay;

    private void Awake()
    {
        LoadToolTipInfos();
    }

    public void LoadToolTipInfos()
    {
        var toolTipInfos = Resources.LoadAll<TooltipInfo>(TOOL_TIP_INFO_FILE_PATH);
        foreach (var toolTipInfo in toolTipInfos)
        {
            tooltips.Add(toolTipInfo);
        }
    }

    public string GetDescriptionFrom(Tag tag)
    {
        foreach (var toolTip in tooltips)
        {
            if (toolTip.tag.Equals(tag))
            {
                return toolTip.description;
            }
        }

        return "";
    }

    public int GetFontSizeFrom(Tag tag)
    {
        foreach (var toolTip in tooltips)
        {
            if (toolTip.tag.Equals(tag))
            {
                return toolTip.fontSize;
            }
        }

        return 6;
    }

    public void Toggle()
    {
        Clear();
        isDisabled = !isDisabled;
    }

    private TextMeshPro GetText()
    {
        if (toolTip == null)
        {
            var toolTipObject = Instantiate(toolTipPrefab, transform);
            toolTip = toolTipObject.GetComponentInChildren<TextMeshPro>();
        }

        return toolTip;
    }

    private TextMeshPro GetEffectText()
    {
        var effectToolTip = CreateToolTip();
        effectTips.Add(effectToolTip);
        return effectToolTip;
    }

    private TextMeshPro CreateToolTip()
    {
        var effectTipObject = Instantiate(effectToolTipPrefab, transform);
        effectTipObject.GetComponent<SpriteRenderer>().color = Color.green;
        return effectTipObject.GetComponentInChildren<TextMeshPro>();
    }

    private Transform GetTextParent() { return GetText().transform.parent; }

    public string GetToolTip() { return GetText().text; }

    public void SetText(CardInfo cardInfo)
    {
        if (cardInfo.HasNoEffect())
        {
            return;
        }

        GetText().text = cardInfo.GetDescription();
    }

    private bool IsSamePosition(Vector3 positon) { return GetTextParent().position == positon; }

    private bool NewCardIsSameAsCurrent(CardInfo cardInfo, Vector3 position)
    {
        return IsSamePosition(position) && cardInfo.Equals(currentlyDisplay);
    }

    private bool ShouldNotDisplayNewCard(CardInfo cardInfo, Vector3 position)
    {
        return isDisabled || NewCardIsSameAsCurrent(cardInfo, position);
    }

    private void SetCurrentlyDisplayed(CardInfo cardInfo) { currentlyDisplay = cardInfo; }

    private void SetPosition(Vector3 position)
    {
        GetTextParent().position = position;
    }

    private void Show() { GetTextParent().gameObject.SetActive(true); }

    private void Hide() { GetTextParent().gameObject.SetActive(false); }

    private void HideEffect()
    {
        foreach (var effects in effectTips)
        {
            Destroy(effects.transform.parent.gameObject);
        }

        effectTips = new List<TextMeshPro>();
    }

    public void Set(CardInfo cardInfo, Vector3 position)
    {
        if (ShouldNotDisplayNewCard(cardInfo, position))
        {
            return;
        }

        HideEffect();
        SetCurrentlyDisplayed(cardInfo);
        SetPosition(position);

        if (currentlyDisplay.HasNoEffect())
        {
            Hide();
            return;
        }

        Show();
        SetText(currentlyDisplay);
        SetExtraText(cardInfo, position);
    }

    private bool ShouldNotDisplayExtra(CardInfo cardInfo)
    {
        return cardInfo.tags.Count <= 1;
    }

    public void SetExtraText(CardInfo cardInfo, Vector3 position)
    {
        if (ShouldNotDisplayExtra(cardInfo))
        {
            return;
        }

        for (int i = 1; i < cardInfo.tags.Count; i++)
        {
            var extraTextOffset = GetOffset(position, i - 1, cardInfo.tags.Count - 1);
            CreateExtraText(cardInfo.tags[i], extraTextOffset);
        }
    }

    private Vector3 GetOffset(Vector3 position, int i, int size)
    {
        var positionMaker = new BlockCenterPosition(position + GetExtraEffectOffset(position),
                                                    size,
                                                    NUMBER_OF_EXTRA_TOOL_TIPS_IN_COLUMN,
                                                    EXTRA_TOOL_TIP_SPACING,
                                                    EFFECT_TEXT_POSITION_OFFSET_MULTIPLE.y);
        return positionMaker.GetVerticalLayoutPositionAt(size - 1 - i);
    }

    private Vector3 GetExtraEffectOffset(Vector3 position)
    {
        if (PositionIsOnRightSideOfScreen(position))
        {
            return -1 * EFFECT_TEXT_POSITION_OFFSET;
        }

        return EFFECT_TEXT_POSITION_OFFSET;
    }

    private bool PositionIsOnRightSideOfScreen(Vector3 position)
    {
        var camera = Camera.main;
        var screenPosition = camera.WorldToScreenPoint(position);
        if (screenPosition.x > (Screen.width * 2 / 3))
        {
            return true;
        }

        return false;
    }

    private void CreateExtraText(Tag tag, Vector3 position)
    {
        var effectText = GetEffectText();
        effectText.text = GetDescriptionFrom(tag);
        effectText.fontSize = GetFontSizeFrom(tag);
        effectText.transform.parent.position = position;
    }

    private bool NothingIsDisplayed() { return currentlyDisplay == null; }

    public void Clear()
    {
        if (NothingIsDisplayed())
        {
            return;
        }

        currentlyDisplay = null;
        Hide();
        HideEffect();
    }
}
