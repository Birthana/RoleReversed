using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DescriptionUI : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private TMP_Text text;

    public void SetText()
    {
        SetActive(true);
        text.text = new DescriptionText().GetText(text.text);
    }

    public void SetActive(bool active)
    {
        background.gameObject.SetActive(active);
        text.gameObject.SetActive(active);
    }
}
