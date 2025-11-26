using System;
using UnityEngine;
using UnityEngine.UI;

public class Entity_Health : MonoBehaviour
{
    private Slider healthBar;
    private Entity entity;
    private Entity_VFX entityVfx;
    private Entity_Stats stats;

    [SerializeField] protected float currentHp;
    [SerializeField] protected bool isDead;

    protected bool isInvulnerable = false;



    protected virtual void Awake()
    {
        entityVfx = GetComponent<Entity_VFX>();
        entity = GetComponent<Entity>();
        stats = GetComponent<Entity_Stats>();
        healthBar = GetComponentInChildren<Slider>();

        float maxHp;

        if (stats != null)
        {
            maxHp = stats.GetMaxHealth();
        }
        else
        {
            maxHp = currentHp;
        }

        currentHp = maxHp;
        UpdatehealthBar();
    }

    public void Heal(float amount)
    {
        if (isDead) return;

        currentHp += amount;

        float maxHp;

        if (stats != null)
        {
            maxHp = stats.GetMaxHealth();
        }
        else
        {
            maxHp = currentHp;
        }

        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }

        UpdatehealthBar();
    }

    public void RecalculateMaxHealth(bool fullHeal)
    {
        if (stats == null) return;

        float maxHp = stats.GetMaxHealth();

        if (fullHeal)
            currentHp = maxHp;
        else
            currentHp = Mathf.Min(currentHp, maxHp);   

        UpdatehealthBar();
    }

    public void IncreaseMaxHpAndHeal(float addedHp)
    {
        if (stats == null)
            return;

        float newMaxHp = stats.GetMaxHealth();

        currentHp += addedHp;

        if (currentHp > newMaxHp)
        {
            currentHp = newMaxHp;
        }

        UpdatehealthBar();
    }

    public virtual void TakeDamage(float damage)
    {
        if (isDead || isInvulnerable)
        {
            return;
        }

        entityVfx?.PlayOnDamageVfx();
        ReduceHp(damage);
    }

    protected void ReduceHp(float damage)
    {
        currentHp = currentHp - damage;
        UpdatehealthBar();
        if (currentHp <= 0)
        {
            currentHp = 0;
            Die();
        }
    }

    protected virtual void Die()
    {
        isDead = true;
        if (entity != null)
            entity.EntityDeath();
    }
    public void SetInvulnerable(bool value)
    {
        isInvulnerable = value;
    }

    private void UpdatehealthBar()
    {
        healthBar.value = currentHp / stats.GetMaxHealth();
    }

    public bool IsDead => isDead;
}
