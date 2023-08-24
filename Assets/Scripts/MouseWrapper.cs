using UnityEngine;

public class MouseWrapper : IMouseWrapper
{
    public bool PlayerPressesLeftClick() { return Mouse.PlayerPressesLeftClick(); }

    public bool PlayerReleasesLeftClick() { return Mouse.PlayerReleasesLeftClick(); }

    public bool IsOnHand() { return Mouse.IsOnHand(); }

    public ComponentType GetHitComponent<ComponentType>() { return Mouse.GetHitComponent<ComponentType>(); }

    public bool IsOnSelection() { return Mouse.IsOnSelection(); }

    public Vector2 GetPosition() { return Mouse.GetPosition(); }
}
