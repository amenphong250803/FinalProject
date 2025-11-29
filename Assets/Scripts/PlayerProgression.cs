using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Entity_Stats))]
public class PlayerProgression : MonoBehaviour
{
    [Header("Level Settings")]
    public int currentLevel = 1;

    [Header("EXP")]
    public int expPerKill = 1;
    public int expToNextLevel = 4;

    public int currentExp = 0;

    [Header("Upgrade Per Level")]
    public float strengthPerLevel = 5f;

    [Header("UI")]
    public Slider expBar;

    private Entity_Stats stats;

    private void Awake()
    {
        stats = GetComponent<Entity_Stats>();
    }

    private void Start()
    {
        if (expBar != null)
        {
            expBar.maxValue = expToNextLevel;
            expBar.value = currentExp;
        }
    }

    public void AddKill(int amount = 1)
    {
        currentExp += amount;

        if (expBar != null)
            expBar.value = currentExp;

        if (currentExp >= expToNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        currentLevel++;

        stats.major.strength.AddModifier(strengthPerLevel);

        if (FloatingTextManager.Instance != null)
        {
            FloatingTextManager.Instance.ShowText(
                $"LEVEL UP! +{strengthPerLevel} STR",
                transform.position + Vector3.up * 2f,
                Color.yellow
            );
        }

        currentExp = 0;

        if (expBar != null)
        {
            expBar.value = currentExp;
        }

    }

    public void LoadFromSave(int savedLevel, int savedExp)
    {
        currentLevel = Mathf.Max(1, savedLevel);
        currentExp = Mathf.Clamp(savedExp, 0, expToNextLevel);

        if (expBar != null)
        {
            expBar.maxValue = expToNextLevel;
            expBar.value = currentExp;
        }
    }
}
