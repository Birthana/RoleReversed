using System.Collections.Generic;
using UnityEngine;

public class DisplayObject : MonoBehaviour
{
    protected IMouseWrapper mouse;
    [SerializeField] protected List<CardInfo> cardInfos = new List<CardInfo>();
    private DisplayCardInfos displayCardInfos;

    public void SetMouse(IMouseWrapper mouse)
    {
        this.mouse = mouse;
    }

    protected void Awake()
    {
        SetMouse(new MouseWrapper());
    }

    private DisplayCardInfos GetDisplayCardInfos()
    {
        if (displayCardInfos == null)
        {
            displayCardInfos = FindObjectOfType<DisplayCardInfos>();
        }

        return displayCardInfos;
    }

    protected virtual bool PlayerClicksOnObject() { return false; }

    public void Update()
    {
        if (PlayerClicksOnObject() && !GetDisplayCardInfos().IsDisplay())
        {
            GetDisplayCardInfos().Show(cardInfos);
            return;
        }

        if (PlayerClicksOnObject() && GetDisplayCardInfos().IsDisplay())
        {
            GetDisplayCardInfos().Hide();
            return;
        }
    }
}
