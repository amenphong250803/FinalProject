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
        return damage.GetValue() + major.strength.GetValue();
    }

    public float GetVitalityTotal()
    {
        return major.vitality.GetValue();
    }

    public void SetVitalityFromSave(float totalVitality)
    {
        major.vitality.SetBaseValue(totalVitality);
    }

    public float GetStrengthTotal()
    {
        return major.strength.GetValue();
    }

    public void SetStrengthFromSave(float totalStrength)
    {
        major.strength.SetBaseValue(totalStrength);
    }
}

