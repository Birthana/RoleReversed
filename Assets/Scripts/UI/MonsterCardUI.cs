using UnityEngine;
using TMPro;

public class MonsterCardUI : MonoBehaviour
{
    [SerializeField] private BasicUI cost;
    [SerializeField] private TextMeshPro description;
    [SerializeField] private SpriteRenderer cardSprite;
    [SerializeField] private BasicUI damage;
    [SerializeField] private BasicUI health;

    public BasicUI GetDamageUI() { return damage; }

    public BasicUI GetHealthUI() { return health; }

    public SpriteRenderer GetCardSprite() { return cardSprite; }

    public void SetCardInfo(MonsterCardInfo newCardInfo)
    {
        SetDamage(newCardInfo.damage);
        SetHealth(newCardInfo.health);
        SetCost(newCardInfo.cost);
        var effectText = new EffectText();
        SetDescription(effectText.GetText(newCardInfo.effectDescription));
        SetCardSprite(newCardInfo.fieldSprite);
    }

    private void SetDamage(int cardDamage)
    {
        if (damage == null)
        {
            return;
        }

        damage.Display(cardDamage);
    }

    private void SetHealth(int cardHealth)
    {
        if (health == null)
        {
            return;
        }

        health.Display(cardHealth);
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
