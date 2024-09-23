using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoosterToolTip : MonoBehaviour
{
    [SerializeField] private RectTransform ui;
    [SerializeField] private TextMeshProUGUI boosterName;
    [SerializeField] private TextMeshProUGUI boosterDescription;
    [SerializeField] private List<string> highlightedWords = new List<string>();

    private void Awake()
    {
        Hide();
    }

    public void Display(string name, string description)
    {
        ui.gameObject.SetActive(true);
        SetText(name, description);
    }

    public void MoveTo(Vector3 position)
    {
        ui.gameObject.transform.position = position;
        ui.pivot = new Vector2(GetX(position.x), GetY(position.y));
    }

    private float GetX(float x)
    {
        return x / Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;
    }

    private float GetY(float y)
    {
        return y / Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y;
    }

    public void Hide()
    {
        ui.gameObject.SetActive(false);
    }

    private void SetText(string name, string description)
    {
        var highlightedDescription = Highlight(description);
        boosterName.text = name;
        boosterDescription.text = highlightedDescription;
    }

    private string Highlight(string description)
    {
        string highlightedDescription = "";

        foreach (var word in description.Split(' '))
        {
            if (ShouldBeHighlighted(word))
            {
                highlightedDescription += $"<b><color=yellow>{word}</color></b> ";
                continue;
            }

            highlightedDescription += word + " ";
        }

        return highlightedDescription;
    }

    private bool ShouldBeHighlighted(string word)
    {
        foreach (var importantWord in highlightedWords)
        {
            if (importantWord.Equals(word))
            {
                return true;
            }
        }

        return false;
    }
}
