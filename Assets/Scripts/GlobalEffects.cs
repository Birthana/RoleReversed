using System;
using UnityEngine;

public class GlobalEffects : MonoBehaviour
{
    private event Action<Monster> OnEntrance;
    private event Action<Monster> OnEngage;
    private event Action<Monster> OnExit;

    public void AddToEntrance(Action<Monster> function) { OnEntrance += function; }

    public void AddToEngage(Action<Monster> function) { OnEngage += function; }

    public void AddToExit(Action<Monster> function) { OnExit += function; }

    public void Entrance(Monster monster)
    {
        OnEntrance?.Invoke(monster);
    }

    public void Engage(Monster monster)
    {
        OnEngage?.Invoke(monster);
    }

    public void Exit(Monster monster)
    {
        OnExit?.Invoke(monster);
    }
}
