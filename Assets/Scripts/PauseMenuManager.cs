using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject pauseMenuPanel;

    [Header("Refs")]
    public PlayerSaveController playerSaveController;

    [Header("Scenes")]
    public string mainMenuSceneName = "MainMenu";

    private bool isPaused = false;

    private void Update()
    {
        // Bấm ESC để bật/tắt menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        if (pauseMenuPanel != null)
            pauseMenuPanel.SetActive(true);

        Time.timeScale = 0f; // dừng game
    }

    public void ResumeGame()
    {
        isPaused = false;
        if (pauseMenuPanel != null)
            pauseMenuPanel.SetActive(false);

        Time.timeScale = 1f; // chạy lại game
    }

    public void OnSaveAndExit()
    {
        // 1. Lưu game
        if (playerSaveController != null)
        {
            playerSaveController.SaveGame();
        }

        // 2. Reset time scale về bình thường
        Time.timeScale = 1f;

        // 3. Về MainMenu
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
