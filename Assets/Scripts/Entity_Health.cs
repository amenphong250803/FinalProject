using System;
using UnityEngine;

public class Entity_Health : MonoBehaviour
{
    private Entity entity;
    private Entity_VFX entityVfx;

    [SerializeField] protected float maxHp = 100;
    [SerializeField] protected float currentHp;
    [SerializeField] protected bool isDead;



    protected virtual void Awake()
    {
        entityVfx = GetComponent<Entity_VFX>();
        entity = GetComponent<Entity>();
        currentHp = maxHp;
    }

    public virtual void TakeDamage(float damage)
    {
        if (isDead)
        {
            return;
        }

        entityVfx?.PlayOnDamageVfx();
        ReduceHp(damage);
    }

    protected void ReduceHp(float damage)
    {
        currentHp = currentHp - damage;

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

    public bool IsDead => isDead;
}
