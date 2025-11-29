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

    [Header("SFX")]
    public AudioSource audioSource;
    public AudioClip useSfx;
    public AudioClip addSfx;

    private void Awake()
    {
        stats = GetComponent<Entity_Stats>();
        health = GetComponent<Entity_Health>();
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

        if (audioSource != null && addSfx != null)
            audioSource.PlayOneShot(addSfx);

        UpdatePotionUI();
    }

    private void UsePermanentHp()
    {
        if (currentPermanentItems <= 0)
        {
            return;
        }

        currentPermanentItems--;

        if (audioSource != null && useSfx != null)
            audioSource.PlayOneShot(useSfx);

        float oldMaxHp = stats.GetMaxHealth();
        stats.major.vitality.AddModifier(bonusVitality);

        float newMaxHp = stats.GetMaxHealth();
        float addedHp = newMaxHp - oldMaxHp;
        if (addedHp < 0) addedHp = 0;

        health.IncreaseMaxHpAndHeal(addedHp);

        if (FloatingTextManager.Instance != null)
        {
            FloatingTextManager.Instance.ShowText(
                $"+{addedHp} HP (permanent)",
                transform.position + Vector3.up * 1.5f,
                new Color(0.3f, 0.9f, 1f)
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
