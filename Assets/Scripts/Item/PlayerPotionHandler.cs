using UnityEngine;
using TMPro;

public class PlayerPotionHandler : MonoBehaviour
{
    private Entity_Health health;

    [Header("UI")]
    public TextMeshProUGUI potionText;

    [Header("Potion settings")]
    public int maxPotions = 5;
    public float healAmount = 50f;

    [Header("Current state")]
    public int currentPotions = 0;

    private void Awake()
    {
        health = GetComponent<Entity_Health>();
        if (health == null)
        {
            Debug.LogError("PlayerPotionHandler: Không tìm thấy Entity_Health trên player!");
        }

        UpdatePotionUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            UsePotion();
        }
    }

    public void AddPotion(int amount)
    {
        currentPotions += amount;

        if (currentPotions > maxPotions)
            currentPotions = maxPotions;

        Debug.Log($"Nhặt bình máu, hiện có: {currentPotions}");

        UpdatePotionUI();
    }

    private void UsePotion()
    {
        if (health == null) return;

        if (currentPotions <= 0)
        {
            Debug.Log("Không còn bình máu để dùng!");
            return;
        }

        if (health.IsDead)
        {
            Debug.Log("Chết rồi thì uống gì nữa :))");
            return;
        }

        currentPotions--;
        health.Heal(healAmount);

        Debug.Log($"Dùng 1 bình máu, còn lại: {currentPotions}");

        UpdatePotionUI();
    }

    private void UpdatePotionUI()
    {
        if (potionText != null)
            potionText.text = $"{currentPotions}";
    }
}
