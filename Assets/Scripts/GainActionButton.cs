using UnityEngine;

public class GainActionButton : MonoBehaviour
{
    public SelectionScreen selectionScreen;
    private GainActionManager gainAction;

    private void Start()
    {
        selectionScreen.gameObject.SetActive(false);
        gainAction = FindObjectOfType<GainActionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.PlayerPressesLeftClick() && Mouse.IsOnActionButton())
        {
            selectionScreen.gameObject.SetActive(true);
            selectionScreen.SetMaxSelection(3);
            selectionScreen.SetSelectButton(AddToGainAction);
            selectionScreen.SetCancelButton(Cancel);
        }
    }

    public void AddToGainAction()
    {
        selectionScreen.gameObject.SetActive(false);
        for (int i = 0; i < selectionScreen.GetSelections(); i++)
        {
            gainAction.UseSelectCardToPayForGainAction();
        }

        selectionScreen.DestroyAllSelections();
        selectionScreen.RemoveSelectButton(AddToGainAction);
    }

    public void Cancel()
    {
        selectionScreen.gameObject.SetActive(false);
        selectionScreen.ReturnAllSelections();
        selectionScreen.RemoveCancelButton(Cancel);
    }
}
