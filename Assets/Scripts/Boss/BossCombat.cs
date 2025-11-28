using UnityEngine;

public class BossCombat : MonoBehaviour
{
    [Header("Combat Settings")]
    public float damage = 20f;
    public float damageCooldown = 5f;
    private float nextDamageTime = 0f;

    [Header("Target detection")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius = 1.5f;
    [SerializeField] private LayerMask targetMask;

    private BossHealth bossHealth;

    [Header("SFX Settings")]
    public AudioSource audioSource;     // nơi phát tiếng tấn công
    public AudioClip attackSFX;         // tiếng tấn công của boss

    void Awake()
    {
        bossHealth = GetComponentInParent<BossHealth>();

        if (audioSource == null)
            audioSource = GetComponentInParent<AudioSource>();

        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.spatialBlend = 0;   // 2D sound
    }

    public void PerformAttack()
    {
        if (bossHealth != null && bossHealth.IsDead)
            return;

        PlayAttackSFX();

        if (Time.time < nextDamageTime)
            return;

        Collider2D[] targets = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, targetMask);

        foreach (var target in targets)
        {
            Entity_Health hp = target.GetComponent<Entity_Health>();
            if (hp != null && !hp.IsDead)
            {
                float finalDamage = damage * bossHealth.damageMultiplier;
                hp.TakeDamage(finalDamage);

                Debug.Log("Boss đánh trúng, gây damage: " + finalDamage);
            }
        }

        nextDamageTime = Time.time + damageCooldown;
    }

    private void PlayAttackSFX()
    {
        if (attackSFX != null && audioSource != null)
            audioSource.PlayOneShot(attackSFX);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            Debug.LogError("❌ AttackPoint chưa được gán!");
            return;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
