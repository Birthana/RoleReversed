using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    private CardInfo cardInfo;

    public virtual void SetCardInfo(CardInfo newCardInfo)
    {
        cardInfo = newCardInfo;
        SetSprite();
        SetDescription();
    }

    public void SetSprite()
    {
        var renderer = GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.sprite = cardInfo.cardSprite;
        }
    }

    public void SetDescription()
    {
        var description = GetComponentInChildren<TextMeshPro>();
        if (description != null)
        {
            description.text = cardInfo.effectDescription;
        }
    }

    public int GetCost() { return cardInfo.cost; }

    public string GetName() { return cardInfo.cardName; }

    public virtual bool HasTarget()
    {
        return true;
    }

    public virtual void Cast()
    {
        ReduceActions();
        FindObjectOfType<Hand>().Remove(this);
        Destroy(gameObject);
    }

    private void ReduceActions()
    {
        FindObjectOfType<ActionManager>().ReduceActions(GetCost());
    }
}
