using UnityEngine;

public class Entity_Stats : MonoBehaviour
{
    public Stat_MajorGroup major;

    [Header("Core stats")]
    public Stat maxHp;
    public Stat damage;

    public float GetMaxHealth()
    {
        float baseHp = maxHp.GetValue();
        float bonusHp = major.vitality.GetValue() * 5f;

        return baseHp + bonusHp;
    }

    public float GetDamage()
    {
        return damage.GetValue();
    }
}
