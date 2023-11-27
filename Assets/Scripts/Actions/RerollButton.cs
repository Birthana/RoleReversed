using UnityEngine;

public class RerollButton : MonoBehaviour
{
    public SelectionScreen selectionScreen;
    private RerollManager reroll;

    // Start is called before the first frame update
    void Start()
    {
        selectionScreen.gameObject.SetActive(false);
        reroll = FindObjectOfType<RerollManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.PlayerPressesLeftClick() && Mouse.IsOnRerollButton())
        {
            selectionScreen.gameObject.SetActive(true);
            selectionScreen.SetMaxSelection(2);
            selectionScreen.SetSelectButton(AddToReroll);
            selectionScreen.SetCancelButton(Cancel);
        }
    }

    public void AddToReroll()
    {
        selectionScreen.gameObject.SetActive(false);
        for (int i = 0; i < selectionScreen.GetSelections(); i++)
        {
            reroll.UseSelectedCardToPayForReroll();
        }

        selectionScreen.DestroyAllSelections();
        selectionScreen.RemoveSelectButton(AddToReroll);
    }

    public void Cancel()
    {
        selectionScreen.gameObject.SetActive(false);
        selectionScreen.ReturnAllSelections();
        selectionScreen.RemoveCancelButton(Cancel);
    }
}
