using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuUI : MonoBehaviour
{
    [Header("Refs")]
    public PlayerSaveController playerSaveController;

    [Header("Scenes")]
    public string mainMenuSceneName = "MainMenu";

    public void OnSaveAndExit()
    {
        if (playerSaveController != null)
        {
            playerSaveController.SaveGame();
        }

        Time.timeScale = 1f;

        SceneManager.LoadScene(mainMenuSceneName);
    }
}
