using UnityEngine;

public class Hp_Pernament : MonoBehaviour
{
    public float bonusVitality = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Entity_Stats stats = other.GetComponent<Entity_Stats>();
        Entity_Health health = other.GetComponent<Entity_Health>();

        if (stats == null || health == null)
            return;

        float oldMaxHp = stats.GetMaxHealth();

        stats.major.vitality.AddModifier(bonusVitality);

        float newMaxHp = stats.GetMaxHealth();

        float addedHp = newMaxHp - oldMaxHp;

        if (addedHp < 0)
        {
            addedHp = 0;
        }

        health.IncreaseMaxHpAndHeal(addedHp);

        Destroy(gameObject);
    }
}
