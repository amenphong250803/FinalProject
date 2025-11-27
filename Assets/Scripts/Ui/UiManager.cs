using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [Header("Scene Names")]
    public string gameSceneName = "Map1";

    [Header("UI")]
    public Button continueButton;

    private void Start()
    {
        // Kiểm tra xem có save không
        bool hasSave = PlayerPrefs.GetInt("HasSave", 0) == 1;

        // Nếu không có save → disable Continue
        if (continueButton != null)
            continueButton.interactable = hasSave;
    }

    // Gắn vào nút Start Game
    public void StartGame()
    {
        // Xóa toàn bộ save để đảm bảo chơi mới
        PlayerPrefs.DeleteKey("Player_HP");
        PlayerPrefs.DeleteKey("Player_VIT");
        PlayerPrefs.DeleteKey("Player_Potions");
        PlayerPrefs.DeleteKey("Player_PermanentItems");
        PlayerPrefs.DeleteKey("Player_PosX");
        PlayerPrefs.DeleteKey("Player_PosY");
        PlayerPrefs.DeleteKey("Player_PosZ");
        PlayerPrefs.DeleteKey("HasSave");

        // Load scene chơi game
        SceneManager.LoadScene(gameSceneName);
    }

    // Gắn vào nút Continue
    public void ContinueGame()
    {
        if (PlayerPrefs.GetInt("HasSave", 0) != 1)
        {
            Debug.Log("❌ Không có save để continue");
            return;
        }

        SceneManager.LoadScene(gameSceneName);
    }

    // Nếu bạn có nút Quit
    public void QuitGame()
    {
        Application.Quit();
    }
}
