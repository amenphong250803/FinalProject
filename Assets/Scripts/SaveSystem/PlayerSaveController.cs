using UnityEngine;

[RequireComponent(typeof(Entity_Health))]
[RequireComponent(typeof(Entity_Stats))]
[RequireComponent(typeof(PlayerPotionHandler))]
[RequireComponent(typeof(PlayerProgression))]
public class PlayerSaveController : MonoBehaviour
{
    private Entity_Health health;
    private Entity_Stats stats;
    private PlayerPotionHandler potionHandler;
    private PlayerPermanentHpHandler permanentHandler;
    private PlayerProgression progression;

    [Header("Save Settings")]
    [SerializeField] private bool autoLoadOnStart = true;
    public static bool LoadFromSave = false;

    private void Awake()
    {
        health = GetComponent<Entity_Health>();
        stats = GetComponent<Entity_Stats>();
        potionHandler = GetComponent<PlayerPotionHandler>();
        permanentHandler = GetComponent<PlayerPermanentHpHandler>();
        progression = GetComponent<PlayerProgression>();
    }

    private void Start()
    {
        if (LoadFromSave && PlayerPrefs.HasKey("Player_HP"))
            LoadGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            SaveGame();

        if (Input.GetKeyDown(KeyCode.L))
            LoadGame();
    }

    public void SaveGame()
    {
        PlayerPrefs.SetFloat("Player_HP", health.GetCurrentHp());
        PlayerPrefs.SetFloat("Player_VIT", stats.GetVitalityTotal());
        PlayerPrefs.SetFloat("Player_STR", stats.GetStrengthTotal());

        if (progression != null)
        {
            PlayerPrefs.SetInt("Player_Level", progression.currentLevel);
            PlayerPrefs.SetInt("Player_Exp", progression.currentExp);
        }

        PlayerPrefs.SetInt("Player_Potions", potionHandler.currentPotions);

        if (permanentHandler != null)
            PlayerPrefs.SetInt("Player_PermanentItems", permanentHandler.currentPermanentItems);

        Vector3 pos = transform.position;
        PlayerPrefs.SetFloat("Player_PosX", pos.x);
        PlayerPrefs.SetFloat("Player_PosY", pos.y);
        PlayerPrefs.SetFloat("Player_PosZ", pos.z);

        PlayerPrefs.SetInt("HasSave", 1);
        PlayerPrefs.Save();
    }

    public void LoadGame()
    {
        if (!PlayerPrefs.HasKey("Player_HP"))
            return;

        float savedVit = PlayerPrefs.GetFloat("Player_VIT", stats.GetVitalityTotal());
        stats.SetVitalityFromSave(savedVit);

        float savedStr = PlayerPrefs.GetFloat("Player_STR", stats.GetStrengthTotal());
        stats.SetStrengthFromSave(savedStr);

        health.RecalculateMaxHealth(false);
        float savedHp = PlayerPrefs.GetFloat("Player_HP", health.GetCurrentHp());
        health.SetCurrentHp(savedHp);

        if (progression != null)
        {
            int savedLevel = PlayerPrefs.GetInt("Player_Level", progression.currentLevel);
            int savedExp = PlayerPrefs.GetInt("Player_Exp", 0);
            progression.LoadFromSave(savedLevel, savedExp);
        }

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
    }

    public void SaveGameButton() => SaveGame();
    public void LoadGameButton() => LoadGame();
}
