using System;
using UnityEngine;
using UnityEngine.UI;

public class Entity_Health : MonoBehaviour
{
    private Slider healthBar;
    private Entity entity;
    private Entity_VFX entityVfx;

    [SerializeField] protected float maxHp = 100;
    [SerializeField] protected float currentHp;
    [SerializeField] protected bool isDead;

    protected bool isInvulnerable = false;



    protected virtual void Awake()
    {
        entityVfx = GetComponent<Entity_VFX>();
        entity = GetComponent<Entity>();
        healthBar = GetComponentInChildren<Slider>();

        currentHp = maxHp;
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
        healthBar.value = currentHp / maxHp;
    }

    public bool IsDead => isDead;
}
