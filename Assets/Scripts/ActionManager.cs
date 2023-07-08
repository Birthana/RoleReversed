using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    public int maxActions;
    [SerializeField] private int currentActions;

    private void Awake()
    {
        SetActions(maxActions);
    }

    public void SetActions(int actions)
    {
        currentActions = actions;
    }

    public bool CanCast(Card card)
    {
        return (currentActions != 0) && (card.GetCost() <= currentActions);
    }

    public void ReduceActions(int actions)
    {
        SetActions(Mathf.Max(0, currentActions - actions));
    }
}
