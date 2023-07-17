using UnityEngine;
using TMPro;

public class BasicUI : MonoBehaviour
{
    public TextMeshPro ui;

    public virtual void Display(int health)
    {
        ui.text = $"{health}";
    }
}
