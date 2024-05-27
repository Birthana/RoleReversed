using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolTipManager : MonoBehaviour
{
    public GameObject toolTipPrefab;
    private TextMeshPro toolTip;
    private TextMeshPro effectTip;

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

    public string GetToolTip()
    {
        return GetText().text;
    }

    public void SetText(string text)
    {
        if (text.Equals(""))
        {
            return;
        }

        GetText().text = text;
    }

    public void SetText(string text, Vector3 positon)
    {
        if (text.Equals(""))
        {
            return;
        }

        GetTextParent().gameObject.SetActive(true);
        SetText(text);
        GetTextParent().position = positon;
    }

    public void SetText(string text, Vector3 positon, List<RoommateEffectInfo> effects)
    {
        SetText(text, positon);
        if (effects.Count == 0)
        {
            return;
        }

        GetEffectText().transform.parent.gameObject.SetActive(true);
        GetEffectText().text = effects[0].cardDescription;
        GetEffectText().transform.parent.position = positon + (Vector3.right * 5);
    }

    public void Clear()
    {
        GetTextParent().gameObject.SetActive(false);
        GetEffectText().transform.parent.gameObject.SetActive(false);
    }
}
