using UnityEngine;
using TMPro;


public class RoomCardUI : MonoBehaviour
{
    [SerializeField] private BasicUI cost;
    [SerializeField] private TextMeshPro description;
    [SerializeField] private SpriteRenderer cardSprite;
    [SerializeField] private BasicUI capacity;

    public BasicUI GetCapacity() { return capacity; }

    public SpriteRenderer GetCardSprite() { return cardSprite; }


    public void SetCardInfo(RoomCardInfo newCardInfo)
    {
        SetCost(newCardInfo.cost);
        var effectText = new EffectText();
        SetDescription(effectText.GetText(newCardInfo.effectDescription));
        SetCardSprite(newCardInfo.fieldSprite);
        SetCapacity(newCardInfo.capacity);
    }

    private void SetCapacity(int cardCapacity)
    {
        if (capacity == null)
        {
            return;
        }

        capacity.Display(cardCapacity);
    }

    private void SetCost(int cardCost)
    {
        if (cost == null)
        {
            return;
        }

        cost.Display(cardCost);
    }

    private void SetCardSprite(Sprite sprite)
    {
        if (cardSprite == null)
        {
            return;
        }

        cardSprite.sprite = sprite;
    }

    private void SetDescription(string cardDescription)
    {
        if (description == null)
        {
            return;
        }

        description.text = cardDescription;
    }
}
