using UnityEngine;

public class BossHealth : Entity_Health
{
    public bool isImmune = false;
    public float damageMultiplier = 1f;

    private Animator anim;

    [Header("Music")]
    public BossTargetDetection detectMusic;

    [Header("SFX")]
    public AudioSource audioSource;
    public AudioClip deathSFX;
    private bool deathSoundPlayed = false;

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponentInChildren<Animator>();

        if (audioSource == null)
        {
            audioSource = GetComponentInChildren<AudioSource>();
        }
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

        PlayDeathSFX();

        if (detectMusic != null)
        {
            detectMusic.OnBossDead();
        }

    }

    private void PlayDeathSFX()
    {
        if (deathSoundPlayed) return; 
        if (audioSource == null || deathSFX == null) return;

        audioSource.PlayOneShot(deathSFX);
        deathSoundPlayed = true;
    }

    public void SetImmune(bool value)
    {
        isImmune = value;
    }

    public float GetHpPercent()
    {
        if (currentHp <= 0) return 0f;

        var bar = GetComponentInChildren<UnityEngine.UI.Slider>();
        if (bar == null || bar.maxValue == 0f) return 0f;

        float calculatedMaxHp = currentHp / bar.value;
        return currentHp / calculatedMaxHp;
    }

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

    private void UpdateBar()
    {
        var bar = GetComponentInChildren<UnityEngine.UI.Slider>();
        if (bar != null)
            bar.value = currentHp / (currentHp / bar.value);
    }
}
