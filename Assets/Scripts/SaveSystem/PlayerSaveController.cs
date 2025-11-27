using UnityEngine;

[RequireComponent(typeof(Entity_Health))]
[RequireComponent(typeof(Entity_Stats))]
[RequireComponent(typeof(PlayerPotionHandler))]
public class PlayerSaveController : MonoBehaviour
{
    private Entity_Health health;
    private Entity_Stats stats;
    private PlayerPotionHandler potionHandler;
    private PlayerPermanentHpHandler permanentHandler;

    [Header("Save Settings")]
    [SerializeField] private bool autoLoadOnStart = true;

    private void Awake()
    {
        health = GetComponent<Entity_Health>();
        stats = GetComponent<Entity_Stats>();
        potionHandler = GetComponent<PlayerPotionHandler>();
        permanentHandler = GetComponent<PlayerPermanentHpHandler>();
    }

    private void Start()
    {
        if (autoLoadOnStart && PlayerPrefs.HasKey("Player_HP"))
        {
            LoadGame();
        }
    }

    private void Update()
    {
        // vẫn giữ để test cho dễ
        if (Input.GetKeyDown(KeyCode.K))
            SaveGame();

        if (Input.GetKeyDown(KeyCode.L))
            LoadGame();
    }

    public void SaveGame()
    {
        Debug.Log("Đang chạy SaveGame()");

        PlayerPrefs.SetFloat("Player_HP", health.GetCurrentHp());
        PlayerPrefs.SetFloat("Player_VIT", stats.GetVitalityTotal());

        PlayerPrefs.SetInt("Player_Potions", potionHandler.currentPotions);

        if (permanentHandler != null)
            PlayerPrefs.SetInt("Player_PermanentItems", permanentHandler.currentPermanentItems);

        Vector3 pos = transform.position;
        PlayerPrefs.SetFloat("Player_PosX", pos.x);
        PlayerPrefs.SetFloat("Player_PosY", pos.y);
        PlayerPrefs.SetFloat("Player_PosZ", pos.z);

        PlayerPrefs.SetInt("HasSave", 1);

        PlayerPrefs.Save();
        Debug.Log("💾 Game saved (PlayerPrefs).");
    }

    public void LoadGame()
    {
        Debug.Log("Đang chạy LoadGame()");

        if (!PlayerPrefs.HasKey("Player_HP"))
        {
            Debug.Log("⚠ Chưa có save để load.");
            return;
        }

        float savedVit = PlayerPrefs.GetFloat("Player_VIT", stats.GetVitalityTotal());
        stats.SetVitalityFromSave(savedVit);

        health.RecalculateMaxHealth(false);
        float savedHp = PlayerPrefs.GetFloat("Player_HP", health.GetCurrentHp());
        health.SetCurrentHp(savedHp);

        potionHandler.currentPotions = PlayerPrefs.GetInt("Player_Potions", 0);
        potionHandler.UpdatePotionUI();

        if (permanentHandler != null)
        {
            permanentHandler.currentPermanentItems = PlayerPrefs.GetInt("Player_PermanentItems", 0);
            permanentHandler.UpdatePotionUI();
        }

        float x = PlayerPrefs.GetFloat("Player_PosX", transform.position.x);
        float y = PlayerPrefs.GetFloat("Player_PosY", transform.position.y);
        float z = PlayerPrefs.GetFloat("Player_PosZ", transform.position.z);
        transform.position = new Vector3(x, y, z);

        Debug.Log("📥 Game loaded (PlayerPrefs).");
    }

    // dùng cho UI Button
    public void SaveGameButton() => SaveGame();
    public void LoadGameButton() => LoadGame();
}
