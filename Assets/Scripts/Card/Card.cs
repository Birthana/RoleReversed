using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    [SerializeField] private BasicUI cost;
    [SerializeField] private TextMeshPro description;
    [SerializeField] private SpriteRenderer cardSprite;
    private ActionManager actionManager;
    private Hand hand;

    public ActionManager GetActionManager()
    {
        if (actionManager == null)
        {
            actionManager = FindObjectOfType<ActionManager>();
        }

        return actionManager;
    }

    public Hand GetHand()
    {
        if (hand == null)
        {
            hand = FindObjectOfType<Hand>();
        }

        return hand;
    }

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
        GetActionManager().ReduceActions(GetCost());
        GetHand().Remove(this);
        Destroy(gameObject);
    }
}
