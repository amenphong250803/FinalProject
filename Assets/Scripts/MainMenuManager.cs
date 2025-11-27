using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Scenes")]
    public string gameSceneName = "SampleScene";  // tên scene game của bạn

    [Header("UI")]
    public Button continueButton;

    private void Start()
    {
        // Nếu chưa có save thì disable nút Continue
        bool hasSave = PlayerPrefs.GetInt("HasSave", 0) == 1;
        if (continueButton != null)
            continueButton.interactable = hasSave;
    }

    // Gắn vào nút Start Game
    public void OnStartGame()
    {
        // 🔹 Xóa toàn bộ dữ liệu save để chơi mới
        PlayerPrefs.DeleteKey("Player_HP");
        PlayerPrefs.DeleteKey("Player_VIT");
        PlayerPrefs.DeleteKey("Player_Potions");
        PlayerPrefs.DeleteKey("Player_PermanentItems");
        PlayerPrefs.DeleteKey("Player_PosX");
        PlayerPrefs.DeleteKey("Player_PosY");
        PlayerPrefs.DeleteKey("Player_PosZ");
        PlayerPrefs.DeleteKey("HasSave");

        // Vào lại SampleScene, PlayerSaveController.Start sẽ KHÔNG load nữa
        SceneManager.LoadScene(gameSceneName);
    }

    // Gắn vào nút Continue
    public void OnContinue()
    {
        if (PlayerPrefs.GetInt("HasSave", 0) != 1)
        {
            Debug.Log("❌ Không có save để Continue");
            return;
        }

        // Chỉ cần load scene game, trong đó PlayerSaveController tự autoLoadOnStart
        SceneManager.LoadScene(gameSceneName);
    }

    // (Optional) Gắn vào nút Quit nếu có
    public void OnQuit()
    {
        Application.Quit();
    }
}
