using System;
using UnityEngine;

[RequireComponent(typeof(BasicUI))]
public class ActionManager : MonoBehaviour
{
    public event Action<int> OnActionChange;
    public int maxActions;
    [SerializeField] private int currentActions;

    private void Awake()
    {
        OnActionChange += GetComponent<BasicUI>().Display;
        ResetActions();
    }

    public void ResetActions()
    {
        SetActions(maxActions);
    }

    public void SetActions(int actions)
    {
        currentActions = actions;
        OnActionChange?.Invoke(currentActions);
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
