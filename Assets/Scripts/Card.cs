using UnityEngine;

public class Card : MonoBehaviour
{
    public int cost;

    public int GetCost()
    {
        return cost;
    }

    public bool HasTarget()
    {
        return true;
    }

    public void Cast()
    {
        ReduceActions();
        Destroy(gameObject);
    }

    private void ReduceActions()
    {
        FindObjectOfType<ActionManager>().ReduceActions(cost);
    }
}
