using UnityEngine;

public class MouseWrapper : IMouseWrapper
{
    public bool PlayerPressesLeftClick() { return Mouse.PlayerPressesLeftClick(); }

    public bool PlayerReleasesLeftClick() { return Mouse.PlayerReleasesLeftClick(); }

    public bool IsOnHand() { return Mouse.IsOnHand(); }

    public bool IsOnMonster() { return Mouse.IsOnMonster(); }

    public bool IsOnRoom() { return Mouse.IsOnRoom(); }

    public bool IsOnPack() { return Mouse.IsOnPack(); }

    public bool IsOnDraft() { return Mouse.IsOnDraft(); }

    public ComponentType GetHitComponent<ComponentType>() { return Mouse.GetHitComponent<ComponentType>(); }

    public bool IsOnSelection() { return Mouse.IsOnSelection(); }

    public Vector2 GetPosition() { return Mouse.GetPosition(); }
}
