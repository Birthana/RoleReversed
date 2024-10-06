using UnityEngine;

public class AssignmentUI : MonoBehaviour
{
    [SerializeField] SpriteRenderer assignedUI;

    private void Awake()
    {
        GetComponent<Monster>().AddToOnLock(Show);
        GetComponent<Monster>().AddToOnUnlock(Hide);
        Hide();
    }

    public void Show()
    {
        assignedUI.gameObject.SetActive(true);
    }

    public void Hide()
    {
        assignedUI.gameObject.SetActive(false);
    }
}
