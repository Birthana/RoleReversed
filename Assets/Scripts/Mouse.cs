using UnityEngine;

public static class Mouse
{
    public static Camera camera = Camera.main;
    private static RaycastHit2D hit;

    public static bool PlayerPressesLeftClick() { return Input.GetMouseButtonDown(0); }

    public static bool PlayerReleasesLeftClick() { return Input.GetMouseButtonUp(0); }

    public static Vector2 GetPosition() { return camera.ScreenToWorldPoint(Input.mousePosition); }

    public static RaycastHit2D IsOnLayer(string layerName)
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        hit = Physics2D.Raycast(ray.origin, Vector2.zero, 100, 1 << LayerMask.NameToLayer(layerName));
        return hit;
    }

    public static ComponentType GetHitComponent<ComponentType>() { return hit.transform.GetComponent<ComponentType>(); }

    public static RaycastHit2D IsOnActionButton() { return IsOnLayer("Action"); }
    public static RaycastHit2D IsOnRerollButton() { return IsOnLayer("Reroll"); }
    public static RaycastHit2D IsOnHandButton() { return IsOnLayer("Hand"); }
}
