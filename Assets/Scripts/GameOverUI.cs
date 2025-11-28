using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [Header("UI")]
    public GameObject gameOverPanel;

    [Header("Scenes")]
    public string gameSceneName = "Map1";
    public string mainMenuSceneName = "MainMenu";

    public void ShowGameOver()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Time.timeScale = 0f; // dừng game
    }

    // Gắn vào nút Play Again
    public void OnPlayAgain()
    {
        Time.timeScale = 1f;

        // Nếu muốn chơi lại từ đầu, xóa save giống StartGame:
        PlayerPrefs.DeleteKey("Player_HP");
        PlayerPrefs.DeleteKey("Player_VIT");
        PlayerPrefs.DeleteKey("Player_Potions");
        PlayerPrefs.DeleteKey("Player_PermanentItems");
        PlayerPrefs.DeleteKey("Player_PosX");
        PlayerPrefs.DeleteKey("Player_PosY");
        PlayerPrefs.DeleteKey("Player_PosZ");
        PlayerPrefs.DeleteKey("HasSave");

        SceneManager.LoadScene(gameSceneName);
    }

    // Gắn vào nút Main Menu
    public void OnBackToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
