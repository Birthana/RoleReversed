using UnityEngine;
using TMPro;

public class CardUI : MonoBehaviour
{
    [SerializeField] private BasicUI cost;
    [SerializeField] private TextMeshPro description;
    [SerializeField] private SpriteRenderer cardSprite;

    public SpriteRenderer GetCardSprite() { return cardSprite; }

    public virtual void SetCardInfo(CardInfo newCardInfo)
    {
        SetCost(newCardInfo.cost);
        SetDescription(newCardInfo.GetDescription(), newCardInfo.GetFontSize());
        SetCardSprite(newCardInfo.fieldSprite);
    }

    private void SetCost(int cardCost)
    {
        if (cost == null)
        {
            return;
        }

        cost.Display(cardCost);
    }

    protected void SetCardSprite(Sprite sprite)
    {
        if (cardSprite == null)
        {
            return;
        }

        cardSprite.sprite = sprite;
    }

    protected void SetDescription(string cardDescription, int fontSize)
    {
        if (description == null)
        {
            return;
        }

        var effectText = new EffectText();
        description.text = effectText.GetText(cardDescription);
        description.fontSize = fontSize;
    }

}
