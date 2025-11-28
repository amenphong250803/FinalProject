using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Scenes")]
    public string gameSceneName = "Map1"; 

    [Header("UI")]
    public Button continueButton;

    private void Start()
    {
        bool hasSave = PlayerPrefs.GetInt("HasSave", 0) == 1;
        if (continueButton != null)
            continueButton.interactable = hasSave;
    }

    public void OnStartGame()
    {
        PlayerPrefs.DeleteAll();

        SceneManager.LoadScene(gameSceneName);
    }

    public void OnContinue()
    {
        if (PlayerPrefs.GetInt("HasSave", 0) != 1)
        {
            Debug.Log("❌ Không có save để Continue");
            return;
        }

        SceneManager.LoadScene(gameSceneName);
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
