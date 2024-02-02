using UnityEngine;

public static class Mouse
{
    public static Camera camera = Camera.main;
    private static RaycastHit2D hit;

    private static Camera GetCamera()
    {
        if (camera == null)
        {
            camera = Camera.main;
        }

        return camera;
    }

    public static bool PlayerPressesLeftClick() { return Input.GetMouseButtonDown(0); }

    public static bool PlayerReleasesLeftClick() { return Input.GetMouseButtonUp(0); }

    public static Vector2 GetPosition() { return GetCamera().ScreenToWorldPoint(Input.mousePosition); }

    public static RaycastHit2D IsOnLayer(string layerName) { return PositionIsOnLayer(Input.mousePosition, layerName); }

    private static RaycastHit2D PositionIsOnLayer(Vector3 position, string layerName)
    {
        Ray ray = GetCamera().ScreenPointToRay(position);
        hit = Physics2D.Raycast(ray.origin, Vector2.zero, 100, 1 << LayerMask.NameToLayer(layerName));
        return hit;
    }

    public static ComponentType GetHitComponent<ComponentType>() { return hit.transform.GetComponent<ComponentType>(); }

    public static Transform GetHitTransform() { return hit.transform; }

    public static RaycastHit2D IsOnActionButton() { return IsOnLayer("Action"); }
    public static RaycastHit2D IsOnRerollButton() { return IsOnLayer("Reroll"); }
    public static RaycastHit2D IsOnHand() { return IsOnLayer("Hand"); }
    public static RaycastHit2D IsOnMonster() { return IsOnLayer("Monster"); }
    public static RaycastHit2D IsOnPack() { return IsOnLayer("Pack"); }
    public static RaycastHit2D IsOnDraft() { return IsOnLayer("Draft"); }
    public static RaycastHit2D IsOnOpenSoulShop() { return IsOnLayer("OpenSoulShop"); }
    public static RaycastHit2D IsOnOption() { return IsOnLayer("Option"); }
    public static RaycastHit2D IsOnSpace() { return IsOnLayer("Space"); }
    public static RaycastHit2D IsOnRoom() { return IsOnLayer("Room"); }
    public static RaycastHit2D IsOnSelection() { return IsOnLayer("Selection"); }
    public static RaycastHit2D IsOnSelect() { return IsOnLayer("Select"); }
    public static RaycastHit2D IsOnCancel() { return IsOnLayer("Cancel"); }
}
