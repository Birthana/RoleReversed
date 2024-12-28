using System;
using UnityEngine;

public class GlobalEffects : MonoBehaviour
{
    private event Action<Monster> OnEntrance;
    private event Action<EffectInput> OnEngage;
    private event Action<EffectInput> OnHandEngage;
    private event Action<Monster> OnExit;
    private event Action<Room> OnRoomEntrance;
    private event Action<Card> OnDraftExit;
    public void AddToEntrance(Action<Monster> function) { OnEntrance += function; }

    public void AddToEngage(Action<EffectInput> function) { OnEngage += function; }

    public void AddToHandEngage(Action<EffectInput> function) { OnHandEngage += function; }

    public void AddToExit(Action<Monster> function) { OnExit += function; }

    public void AddToRoomEntrance(Action<Room> function) { OnRoomEntrance += function; }

    public void AddToDraftExit(Action<Card> function) { OnDraftExit += function; }

    public void Entrance(Monster monster)
    {
        OnEntrance?.Invoke(monster);
    }

    public void Engage(EffectInput input)
    {
        OnEngage?.Invoke(input);
    }

    public void HandEngage(EffectInput input)
    {
        OnHandEngage?.Invoke(input);
    }

    public void Exit(Monster monster)
    {
        OnExit?.Invoke(monster);
    }

    public void RoomEntrance(Room room)
    {
        OnRoomEntrance?.Invoke(room);
    }

    public void DraftExit(Card card)
    {
        OnDraftExit?.Invoke(card);
    }
}
