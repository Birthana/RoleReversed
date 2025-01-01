using UnityEngine;

public class TempUI : MonoBehaviour
{
    [SerializeField] SpriteRenderer tempUI;

    private void Awake()
    {
        GetComponent<Monster>().AddToOnTemp(Show);
        GetComponent<Monster>().AddToOnUntemp(Hide);
        Hide();
    }

    public void Show()
    {
        tempUI.gameObject.SetActive(true);
    }

    public void Hide()
    {
        tempUI.gameObject.SetActive(false);
    }
}
