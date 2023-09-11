using UnityEngine;

public interface IMouseWrapper
{
    public bool PlayerPressesLeftClick();

    public bool PlayerReleasesLeftClick();

    public bool IsOnHand();

    public bool IsOnMonster();

    public ComponentType GetHitComponent<ComponentType>();

    public bool IsOnSelection();

    public Vector2 GetPosition();
}

