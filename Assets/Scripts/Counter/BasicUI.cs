using UnityEngine;
using TMPro;

public class BasicUI : MonoBehaviour
{
    public TextMeshPro ui;
    public Color color = Color.white;

    public virtual void Display(int health)
    {
        ui.text = $"{new IconText(color).GetNumbersText("" + health)}";
    }
}
