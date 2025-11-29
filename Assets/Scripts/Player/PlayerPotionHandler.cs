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

        UpdatePotionUI();
    }

    private void UsePotion()
    {
        if (health == null) return;

        if (currentPotions <= 0)
        {
            return;
        }

        if (health.IsDead)
        {
            return;
        }

        currentPotions--;
        health.Heal(healAmount);

        if (FloatingTextManager.Instance != null)
        {
            FloatingTextManager.Instance.ShowText(
                $"+{healAmount} HP",
                transform.position + Vector3.up * 1.5f,
                Color.green
            );
        }

        UpdatePotionUI();
    }

    public void UpdatePotionUI()
    {
        if (potionText != null)
            potionText.text = $"{currentPotions}";
    }
}
