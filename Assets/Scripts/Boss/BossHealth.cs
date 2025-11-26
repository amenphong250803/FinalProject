using UnityEngine;

public class BossHealth : Entity_Health
{
    public bool isImmune = false;
    public float damageMultiplier = 1f;

    private Animator anim;

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponentInChildren<Animator>(); 
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
        anim.SetTrigger("dead"); // Animation chết của boss
    }

    public void SetImmune(bool value)
    {
        isImmune = value;
    }

    public float GetHpPercent()
    {
        // ⭐ Fix: maxHp đã là biến protected trong Entity_Health
        return currentHp / maxHp;
    }

    public void HealPercent(float percent)
    {
        currentHp += maxHp * percent;

        if (currentHp > maxHp)
            currentHp = maxHp;

        UpdateHealthBarSafe(); // gọi update bar
    }

    private void UpdateHealthBarSafe()
    {
        // Chỉ update nếu healthBar tồn tại
        var bar = GetComponentInChildren<UnityEngine.UI.Slider>();
        if (bar != null && maxHp > 0)
            bar.value = currentHp / maxHp;
    }
}
