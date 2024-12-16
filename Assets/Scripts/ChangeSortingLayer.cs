using UnityEngine;
using TMPro;

public class ChangeSortingLayer
{
    private GameObject gameObject;

    public ChangeSortingLayer(GameObject parent)
    {
        gameObject = parent;
    }

    public void SetToCurrentRoom()
    {
        SetSpriteRenderersTo("CurrentRoom");
        SetTextTo(SortingLayer.NameToID("CurrentRoom"));
    }

    public void SetToDefault()
    {
        SetSpriteRenderersTo("Default");
        SetTextTo(SortingLayer.NameToID("Default"));
    }

    public void SetTo(Room room)
    {
        var sortingLayer = room.GetSortingLayer();
        SetSpriteRenderersTo(sortingLayer);
        SetTextTo(SortingLayer.NameToID(sortingLayer));
    }

    private void SetSpriteRenderersTo(string layer)
    {
        var children = gameObject.GetComponentsInChildren<SpriteRenderer>(true);
        foreach (var child in children)
        {
            child.sortingLayerName = layer;
        }
    }

    private void SetTextTo(int layer)
    {
        var texts = gameObject.GetComponentsInChildren<TextMeshPro>(true);
        foreach (var text in texts)
        {
            text.sortingLayerID = layer;
        }
    }
}
