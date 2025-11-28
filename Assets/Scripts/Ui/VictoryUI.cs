using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryUI : MonoBehaviour
{
    [Header("UI")]
    public GameObject winPanel;

    [Header("Scenes")]
    public string gameSceneName = "Map1";
    public string mainMenuSceneName = "MainMenu";

    public void ShowVictory()
    {
        if (winPanel != null)
            winPanel.SetActive(true);

        Time.timeScale = 0f; 
    }

    public void OnPlayAgain()
    {
        Time.timeScale = 1f;
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(gameSceneName);
    }

    public void OnBackToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void OnQuit()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}
