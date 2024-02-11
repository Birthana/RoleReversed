using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    [SerializeField] private BasicUI cost;
    [SerializeField] private TextMeshPro description;
    [SerializeField] private SpriteRenderer cardSprite;

    public virtual void SetCardInfo(CardInfo newCardInfo) { }

    public virtual CardInfo GetCardInfo() { return null; }

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
