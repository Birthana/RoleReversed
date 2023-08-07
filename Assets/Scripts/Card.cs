using UnityEngine;

public class Card : MonoBehaviour
{
    public int cost;
    private Vector2 startPosition;

    public Vector2 GetStartPosition() { return startPosition; }

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
