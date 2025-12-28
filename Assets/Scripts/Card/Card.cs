using System.Collections;
using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    private ActionManager actionManager;
    private Drop drop;
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

    public Drop GetDrop()
    {
        if (drop == null)
        {
            drop = FindObjectOfType<Drop>();
        }

        return drop;
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
        if (gameObject != null)
        {
            DestroyImmediate(gameObject);
        }

        GetDrop().Add(GetCardInfo());
    }
}
