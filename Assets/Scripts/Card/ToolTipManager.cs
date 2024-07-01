using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolTipManager : MonoBehaviour
{
    private static readonly Vector3 EFFECT_TEXT_POSITION_OFFSET = Vector3.right * 5;
    public GameObject toolTipPrefab;
    private TextMeshPro toolTip;
    private TextMeshPro effectTip;
    private Room currentRoom;
    private bool isDisabled = false;
    [SerializeField] private CardInfo currentlyDisplay;

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
        if (effectTip == null)
        {
            var effectTipObject = Instantiate(toolTipPrefab, transform);
            effectTipObject.GetComponent<SpriteRenderer>().color = Color.green;
            effectTip = effectTipObject.GetComponentInChildren<TextMeshPro>();
        }

        return effectTip;
    }

    private Transform GetTextParent() { return GetText().transform.parent; }

    private Transform GetEffectTextParent() { return GetEffectText().transform.parent; }

    public string GetToolTip() { return GetText().text; }

    public void SetText(CardInfo cardInfo)
    {
        if (cardInfo.HasNoEffect())
        {
            return;
        }

        GetText().text = cardInfo.effectDescription;
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

    private void SetPosition(Vector3 position) { GetTextParent().position = position; }

    private void SetEffectTextPosition(Vector3 position)
    {
        GetEffectTextParent().position = position + EFFECT_TEXT_POSITION_OFFSET;
    }

    private void Show() { GetTextParent().gameObject.SetActive(true); }

    private void Hide() { GetTextParent().gameObject.SetActive(false); }

    private void ShowEffect() { GetEffectTextParent().gameObject.SetActive(true); }

    private void HideEffect() { GetEffectTextParent().gameObject.SetActive(false); }

    public void Set(CardInfo cardInfo, Vector3 position)
    {
        if (ShouldNotDisplayNewCard(cardInfo, position))
        {
            return;
        }

        SetCurrentlyDisplayed(cardInfo);
        SetPosition(position);

        if (currentlyDisplay.HasNoEffect())
        {
            Hide();
            return;
        }

        Show();
        SetText(currentlyDisplay);
    }

    private void SetEffectText(List<RoommateEffectInfo> effects) { GetEffectText().text = effects[0].cardDescription; }

    public void SetText(CardInfo cardInfo, Vector3 position, Room room)
    {
        Set(cardInfo, position);
        if (room.GetRoommateEffects().Count == 0)
        {
            return;
        }

        ShowEffect();
        SetEffectText(room.GetRoommateEffects());
        SetEffectTextPosition(position);

        currentRoom = room;
        room.HighlightMonsters();
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

        if (currentRoom == null)
        {
            return;
        }

        currentRoom.ClearMonsterHighlight();
        currentRoom = null;
    }
}
