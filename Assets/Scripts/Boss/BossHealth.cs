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
        anim.SetTrigger("dead");
    }

    public void SetImmune(bool value)
    {
        isImmune = value;
    }

    /// <summary>
    /// Lấy phần trăm HP dựa trên currentHp / maxHp trong healthBar
    /// </summary>
    public float GetHpPercent()
    {
        if (currentHp <= 0) return 0f;

        // ✔ Lấy maxHp bằng cách đọc từ Slider (đã có sẵn trong Entity_Health)
        var bar = GetComponentInChildren<UnityEngine.UI.Slider>();
        if (bar == null || bar.maxValue == 0f) return 0f;

        // bar.value = currentHp/maxHp → maxHp = currentHp/bar.value
        float calculatedMaxHp = currentHp / bar.value;
        return currentHp / calculatedMaxHp;
    }

    /// <summary>
    /// Heal boss theo phần trăm hp tối đa
    /// </summary>
    public void HealPercent(float percent)
    {
        var bar = GetComponentInChildren<UnityEngine.UI.Slider>();
        if (bar == null || bar.maxValue == 0f) return;

        float calculatedMaxHp = currentHp / bar.value;
        float healAmount = calculatedMaxHp * percent;

        currentHp += healAmount;
        if (currentHp > calculatedMaxHp)
            currentHp = calculatedMaxHp;

        UpdateBar();
    }

    /// <summary>
    /// Cập nhật thanh máu giống Entity_Health
    /// </summary>
    private void UpdateBar()
    {
        var bar = GetComponentInChildren<UnityEngine.UI.Slider>();
        if (bar != null)
            bar.value = currentHp / (currentHp / bar.value);
    }
}
