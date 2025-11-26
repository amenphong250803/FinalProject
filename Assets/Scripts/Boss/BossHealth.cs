using UnityEngine;

public class BossHealth : Entity_Health
{
    public bool isImmune = false;
    public float damageMultiplier = 1f;

    private Animator anim;

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponentInChildren<Animator>(); // ⭐ LẤY ANIM Ở CHILD
    }

    public override void TakeDamage(float damage)
    {
        if (isDead || isImmune)
            return;

        base.TakeDamage(damage * damageMultiplier);
    }

    protected override void Die()
    {
        base.Die();
        anim.SetTrigger("dead"); // chạy animation chết
    }

    public void SetImmune(bool value)
    {
        isImmune = value;
    }

    public float GetHpPercent()
    {
        return currentHp / maxHp;
    }

    public void HealPercent(float percent)
    {
        currentHp += maxHp * percent;
        if (currentHp > maxHp)
            currentHp = maxHp;
    }
}
