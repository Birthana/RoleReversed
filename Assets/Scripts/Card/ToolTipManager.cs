using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolTipManager : MonoBehaviour
{
    [SerializeField] private TextMeshPro toolTipText;

    public void Awake()
    {
        GetToolTip().transform.parent.gameObject.SetActive(false);
    }

    private TextMeshPro GetToolTip()
    {
        if (toolTipText == null)
        {
            toolTipText = GetComponentInChildren<TextMeshPro>();
        }

        return toolTipText;
    }

    public void SetToolTipText(string description)
    {
        GetToolTip().text = description;
    }

    public void SetToolTipText(string description, Vector2 position)
    {
        GetToolTip().transform.parent.gameObject.SetActive(true);
        SetToolTipText(description);
        transform.position = position;
    }

    public string GetText()
    {
        return toolTipText.text;
    }

    public void Clear()
    {
        GetToolTip().transform.parent.gameObject.SetActive(false);
        SetToolTipText("");
    }
}
