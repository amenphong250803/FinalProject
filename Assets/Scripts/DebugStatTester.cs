using UnityEngine;

public class DebugStatTester : MonoBehaviour
{
    private Entity_Health health;
    private Entity_Stats stats;

    public float healAmount = 50f;        
    public float bonusVitality = 5f;      

    private void Awake()
    {
        health = GetComponent<Entity_Health>();
        stats = GetComponent<Entity_Stats>();
    }

    private void Update()
    {
        if (health == null)
        {
            Debug.LogWarning("Không tìm th?y Entity_Health trên player!");
            return;
        }

        if (stats == null)
        {
            Debug.LogWarning("Không tìm th?y Entity_Stats trên player!");
            return;
        }

        // H = test bình h?i máu
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("Nh?n H ? Test h?i máu");
            health.Heal(healAmount);
        }

        // J = test t?ng vitality v?nh vi?n
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("Nh?n J ? Test t?ng vitality");

            float oldMax = stats.GetMaxHealth();

            // t?ng vitality nh? item th?t
            stats.major.vitality.AddModifier(bonusVitality);

            float newMax = stats.GetMaxHealth();
            float addedHp = newMax - oldMax;

            if (addedHp < 0)
                addedHp = 0;

            health.IncreaseMaxHpAndHeal(addedHp);

            Debug.Log($"Vitality +{bonusVitality} ? MaxHP {oldMax} ? {newMax} (+{addedHp})");
        }
    }
}
