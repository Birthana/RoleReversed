using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolTipManager : MonoBehaviour
{
    public GameObject toolTipPrefab;
    private TextMeshPro toolTip;
    private TextMeshPro effectTip;
    private List<Monster> monsters;
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

    public string GetToolTip() { return GetText().text; }

    public void SetText(string text)
    {
        if (text.Equals(""))
        {
            return;
        }

        GetText().text = text;
    }

    private bool IsSamePosition(Vector3 positon) { return GetTextParent().position == positon; }

    private bool CardToDisplayIsSameAsCurrent(CardInfo cardInfo) { return cardInfo.Equals(currentlyDisplay); }

    private bool CurrentCardHasNoEffect() { return currentlyDisplay.effectDescription.Equals(""); }

    private bool ShouldNotDisplay() { return CurrentCardHasNoEffect(); }

    public void SetText(CardInfo cardInfo, Vector3 position)
    {
        if (isDisabled)
        {
            return;
        }

        if ((IsSamePosition(position) && CardToDisplayIsSameAsCurrent(cardInfo)))
        {
            return;
        }

        currentlyDisplay = cardInfo;
        Debug.Log($"Setting: {cardInfo} as currentlydisplayed.");

        if (ShouldNotDisplay())
        {
            GetTextParent().position = position;
            GetTextParent().gameObject.SetActive(false);
            return;
        }

        Debug.Log($"Displaying");
        GetTextParent().gameObject.SetActive(true);
        SetText(currentlyDisplay.effectDescription);
        GetTextParent().position = position;
    }

    public void SetText(CardInfo cardInfo, Vector3 position, List<RoommateEffectInfo> effects)
    {
        SetText(cardInfo, position);
        if (effects.Count == 0)
        {
            return;
        }

        GetEffectText().transform.parent.gameObject.SetActive(true);
        GetEffectText().text = effects[0].cardDescription;
        GetEffectText().transform.parent.position = position + (Vector3.right * 5);
    }

    public void SetText(CardInfo cardInfo, Vector3 position, List<RoommateEffectInfo> effects, RoommateRoom roommateRoom)
    {
        SetText(cardInfo, position, effects);

        if (roommateRoom.room != null && monsters == null)
        {
            monsters = roommateRoom.monsters;
            monsters[0].Highlight();
            monsters[1].Highlight();
        }
    }

    public void Clear()
    {
        if (currentlyDisplay == null)
        {
            return;
        }

        Debug.Log($"Clearing {currentlyDisplay} from currentlydisplay");
        currentlyDisplay = null;
        GetTextParent().gameObject.SetActive(false);
        GetEffectText().transform.parent.gameObject.SetActive(false);
        if (monsters != null)
        {
            monsters[0].UnHighlight();
            monsters[1].UnHighlight();
            monsters = null;
        }
    }
}
