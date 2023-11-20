using UnityEngine;
using TMPro;

#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
public class Card : MonoBehaviour
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
{
    public string cardName = "DEFAULT";
    public int cost;
    private CardInfo cardInfo;

    public virtual void SetCardInfo(CardInfo newCardInfo)
    {
        cardInfo = newCardInfo;
        cardName = cardInfo.cardName;
        cost = cardInfo.cost;
        GetComponent<SpriteRenderer>().sprite = cardInfo.sprite;
        var description = GetComponentInChildren<TextMeshPro>();
        if (description != null)
        {
            description.text = cardInfo.effectDescription;
        }
    }

    public override bool Equals(object other)
    {
        if(other == null)
        {
            return false;
        }

        if(!(other is Card))
        {
            return false;
        }

        return cardName.Equals(((Card)other).cardName);
    }

    public bool Equals_(object other)
    {
        return cardInfo.Equals(other);
    }

    public int GetCost() { return cost; }
    public int GetCost_() { return cardInfo.cost; }

    public string GetName() { return cardName; }
    public string GetName_() { return cardInfo.cardName; }

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
        FindObjectOfType<ActionManager>().ReduceActions(cost);
    }
}
