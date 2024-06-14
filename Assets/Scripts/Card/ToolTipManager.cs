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

    public void SetText(string text, Vector3 position)
    {
        if (text.Equals("") || isDisabled || IsSamePosition(position))
        {
            return;
        }

        GetTextParent().gameObject.SetActive(true);
        SetText(text);
        GetTextParent().position = position;
    }

    public void SetText(string text, Vector3 position, List<RoommateEffectInfo> effects)
    {
        SetText(text, position);
        if (effects.Count == 0)
        {
            return;
        }

        GetEffectText().transform.parent.gameObject.SetActive(true);
        GetEffectText().text = effects[0].cardDescription;
        GetEffectText().transform.parent.position = position + (Vector3.right * 5);
    }

    public void SetText(string text, Vector3 position, List<RoommateEffectInfo> effects, RoommateRoom roommateRoom)
    {
        SetText(text, position, effects);

        if (roommateRoom.room != null && monsters == null)
        {
            monsters = roommateRoom.monsters;
            monsters[0].Highlight();
            monsters[1].Highlight();
        }
    }

    public void Clear()
    {
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
