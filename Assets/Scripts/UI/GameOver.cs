using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI text;

    public void Reload()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowWon()
    {
        text.text = "Game Won!";
        panel.SetActive(true);
    }

    public void ShowLose()
    {
        text.text = "Game Over";
        panel.SetActive(true);
    }

    public void Hide()
    {
        panel.SetActive(false);
    }
}
