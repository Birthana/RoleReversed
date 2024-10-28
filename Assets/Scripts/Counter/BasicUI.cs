using UnityEngine;
using TMPro;

public class BasicUI : MonoBehaviour
{
    public TMP_Text ui;
    public Color color = Color.white;
    private IconText iconText;

    public IconText GetIconText()
    {
        if (iconText == null)
        {
            iconText = new IconText(color);
        }

        return iconText;
    }

    public virtual void Display(int health)
    {
        ui.text = $"{GetIconText().GetNumbersText("" + health)}";
    }
}
