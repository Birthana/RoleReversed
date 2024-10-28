using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public RectTransform bar;
    public BasicUI ui;

    private void Awake()
    {
        ui.gameObject.SetActive(false);
    }

    public BasicUI GetHealthUI()
    {
        ui.gameObject.SetActive(true);
        return ui; 
    }

    public void Display(int currentHealth, int maxHealth)
    {
        var ratio = ((float)currentHealth) / ((float)maxHealth);
        bar.localScale = new Vector3(ratio, bar.localScale.y, bar.localScale.z);
    }

    public void Show() { bar.parent.gameObject.SetActive(true); }

    public void Hide() { bar.parent.gameObject.SetActive(false); }
}
