using TMPro;
using UnityEngine;

public class PlayerPermanentHpHandler : MonoBehaviour
{
    private Entity_Stats stats;
    private Entity_Health health;

    [Header("UI")]
    public TextMeshProUGUI potionText;

    [Header("Permanent HP settings")]
    public int maxPermanentItems = 5;
    public float bonusVitality = 5f;

    [Header("Current state")]
    public int currentPermanentItems = 0;

    private void Awake()
    {
        stats = GetComponent<Entity_Stats>();
        health = GetComponent<Entity_Health>();

        if (stats == null || health == null)
        {
            Debug.LogError("PlayerPermanentHpHandler: thiếu Entity_Stats hoặc Entity_Health!");
        }
        UpdatePotionUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            UsePermanentHp();
        }
    }

    public void AddPermanentHpItem(int amount)
    {
        currentPermanentItems += amount;
        if (currentPermanentItems > maxPermanentItems)
            currentPermanentItems = maxPermanentItems;

        Debug.Log($"Nhặt bình tăng HP, hiện có: {currentPermanentItems}");
        UpdatePotionUI();
    }

    private void UsePermanentHp()
    {
        if (currentPermanentItems <= 0)
        {
            Debug.Log("Không còn bình tăng HP để dùng!");
            return;
        }

        currentPermanentItems--;

        float oldMaxHp = stats.GetMaxHealth();

        stats.major.vitality.AddModifier(bonusVitality);

        float newMaxHp = stats.GetMaxHealth();
        float addedHp = newMaxHp - oldMaxHp;
        if (addedHp < 0) addedHp = 0;

        health.IncreaseMaxHpAndHeal(addedHp);

        Debug.Log($"Dùng bình tăng HP: MaxHP {oldMaxHp} → {newMaxHp} (+{addedHp})");
        if (FloatingTextManager.Instance != null)
        {
            FloatingTextManager.Instance.ShowText(
                $"+{addedHp} HP (permanent)",
                transform.position + Vector3.up * 1.5f,
                new Color(0.3f, 0.9f, 1f) // xanh cyan
            );
        }
        UpdatePotionUI();
    }

    public void UpdatePotionUI()
    {
        if (potionText != null)
            potionText.text = $"{currentPermanentItems}";
    }
}
