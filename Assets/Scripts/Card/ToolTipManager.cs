using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolTipManager : MonoBehaviour
{
    public GameObject toolTipPrefab;
    private TextMeshPro toolTip;

    private TextMeshPro GetText()
    {
        if (toolTip == null)
        {
            var toolTipObject = Instantiate(toolTipPrefab, transform);
            toolTip = toolTipObject.GetComponentInChildren<TextMeshPro>();
        }

        return toolTip;
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

    public void Clear()
    {
        GetTextParent().gameObject.SetActive(false);
    }
}
