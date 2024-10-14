using System;
using UnityEngine;

public class GlobalEffects : MonoBehaviour
{
    private event Action<Monster> OnEntrance;
    private event Action<Monster, Card> OnEngage;
    private event Action<Monster, Card> OnHandEngage;
    private event Action<Monster> OnExit;

    public void AddToEntrance(Action<Monster> function) { OnEntrance += function; }

    public void AddToEngage(Action<Monster, Card> function) { OnEngage += function; }

    public void AddToHandEngage(Action<Monster, Card> function) { OnHandEngage += function; }

    public void AddToExit(Action<Monster> function) { OnExit += function; }

    public void Entrance(Monster monster)
    {
        OnEntrance?.Invoke(monster);
    }

    public void Engage(Monster monster, Card cardSelf)
    {
        OnEngage?.Invoke(monster, cardSelf);
    }

    public void HandEngage(Monster monster, Card cardSelf)
    {
        OnHandEngage?.Invoke(monster, cardSelf);
    }

    public void Exit(Monster monster)
    {
        OnExit?.Invoke(monster);
    }
}
