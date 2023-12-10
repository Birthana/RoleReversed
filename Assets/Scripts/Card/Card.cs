using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    public virtual void SetCardInfo(CardInfo newCardInfo) {}

    public void SetSprite(Sprite sprite)
    {
        var renderer = GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.sprite = sprite;
        }
    }

    public void SetDescription(string effectDescription)
    {
        var description = GetComponentInChildren<TextMeshPro>();
        if (description != null)
        {
            description.text = effectDescription;
        }
    }

    public virtual int GetCost() { return 0; }

    public virtual string GetName() { return ""; }

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
