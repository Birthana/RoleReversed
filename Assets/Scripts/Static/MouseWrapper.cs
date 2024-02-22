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

    public bool IsOnOpenSoulShop() { return Mouse.IsOnOpenSoulShop(); }

    public bool IsOnOption() { return Mouse.IsOnOption(); }

    public bool IsOnDeck() { return Mouse.IsOnDeck(); }

    public bool IsOnDrop() { return Mouse.IsOnDrop(); }

    public ComponentType GetHitComponent<ComponentType>() { return Mouse.GetHitComponent<ComponentType>(); }

    public Transform GetHitTransform() { return Mouse.GetHitTransform(); }

    public bool IsOnSelection() { return Mouse.IsOnSelection(); }

    public Vector2 GetPosition() { return Mouse.GetPosition(); }
}
