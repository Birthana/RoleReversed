using UnityEngine;

public class HoverCard
{
    private static readonly Vector2 HOVER_AMOUNT = new Vector2(0, 3.0f);
    private static readonly Vector3 TOOLTIP_AMOUNT = Vector3.up * 5.0f;
    private static readonly float HOVER_SPEED = 0.1f;
    private Card currentCard;
    private ToolTipManager toolTipManager;
    private HoverAnimation hover;

    public HoverCard(ToolTipManager toolTip, HoverAnimation anim)
    {
        currentCard = null;
        toolTipManager = toolTip;
        hover = anim;
    }

    public Card Get() { return currentCard; }

    public void Hover(Card newCard)
    {
        if (currentCard != null)
        {
            toolTipManager.Clear();
            hover.PerformReturn();
        }

        toolTipManager.Clear();
        currentCard = newCard;
        ShowCardInToolTip();
        hover.Hover(currentCard, HOVER_AMOUNT, HOVER_SPEED);
    }

    public void ShowCardInToolTip()
    {
        var toolTipPosition = currentCard.transform.position + TOOLTIP_AMOUNT;
        var currentCardInfo = GetInfo(); toolTipManager.SetText(currentCardInfo.effectDescription, toolTipPosition);
    }

    private CardInfo GetInfo() { return currentCard.GetCardInfo(); }

    public void Reset()
    {
        hover.ResetHoverAnimation();
        currentCard = null;
    }
}
