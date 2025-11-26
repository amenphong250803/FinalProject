using UnityEngine;

public class BossCombat : MonoBehaviour
{
    [Header("Combat Settings")]
    public float damage = 20f;
    public float damageCooldown = 5f;       // Boss chỉ đánh trúng 5s 1 lần (nếu muốn)
    private float nextDamageTime = 0f;

    [Header("Target detection")]
    [SerializeField] private Transform attackPoint;        // điểm trung tâm vòng tròn
    [SerializeField] private float attackRadius = 1.5f;    // bán kính vùng đánh
    [SerializeField] private LayerMask targetMask;         // layer Player

    private BossHealth bossHealth;

    void Awake()
    {
        bossHealth = GetComponentInParent<BossHealth>();
    }


    // gọi bằng Animation Event
    public void PerformAttack()
    {
        // Nếu đang cooldown -> không gây damage
        if (Time.time < nextDamageTime) return;

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

        // reset cooldown
        nextDamageTime = Time.time + damageCooldown;
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
