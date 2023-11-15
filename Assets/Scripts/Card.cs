using UnityEngine;

#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
public class Card : MonoBehaviour
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
{
    public string cardName = "DEFAULT";
    public int cost;

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

    public int GetCost()
    {
        return cost;
    }

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
