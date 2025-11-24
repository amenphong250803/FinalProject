using System;
using UnityEngine;

public class Entity_Health : MonoBehaviour
{
    private Entity entity;

    [SerializeField] protected float maxHp = 100;
    [SerializeField] protected float currentHp;
    [SerializeField] protected bool isDead;

    protected virtual void Awake()
    {
        entity = GetComponent<Entity>();
        currentHp = maxHp;
    }

    public virtual void TakeDamage(float damage)
    {
        if (isDead)
        {
            return;
        }

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
