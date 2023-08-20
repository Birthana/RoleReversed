using UnityEngine;

public interface IMouseWrapper
{
    public bool PlayerPressesLeftClick();

    public bool PlayerReleasesLeftClick();

    public bool IsOnHand();

    public ComponentType GetHitComponent<ComponentType>();

    public bool IsOnSelection();
}

