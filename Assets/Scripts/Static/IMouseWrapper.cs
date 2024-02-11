using UnityEngine;

public interface IMouseWrapper
{
    public bool PlayerPressesLeftClick();

    public bool PlayerReleasesLeftClick();

    public bool IsOnHand();

    public bool IsOnMonster();

    public bool IsOnRoom();

    public bool IsOnPack();

    public bool IsOnDraft();

    public bool IsOnOpenSoulShop();

    public bool IsOnOption();

    public bool IsOnDeck();

    public bool IsOnDrop();

    public ComponentType GetHitComponent<ComponentType>();

    public bool IsOnSelection();

    public Vector2 GetPosition();
}

