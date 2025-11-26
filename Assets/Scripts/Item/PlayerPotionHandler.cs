using UnityEngine;

public class PlayerPotionHandler : MonoBehaviour
{
    private Entity_Health health;

    [Header("Potion settings")]
    public int maxPotions = 5;       // tối đa bao nhiêu bình
    public float healAmount = 50f;   // mỗi bình hồi bao nhiêu máu

    [Header("Current state")]
    public int currentPotions = 0;   // số bình hiện có (cho inspector nhìn chơi)

    private void Awake()
    {
        health = GetComponent<Entity_Health>();
        if (health == null)
        {
            Debug.LogError("PlayerPotionHandler: Không tìm thấy Entity_Health trên player!");
        }
    }

    private void Update()
    {
        // Bấm E để uống bình
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

        // Uống bình
        currentPotions--;
        health.Heal(healAmount);

        Debug.Log($"Dùng 1 bình máu, còn lại: {currentPotions}");
    }
}

