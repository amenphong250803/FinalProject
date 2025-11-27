using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [Header("Combat Settings")]
    public float damage = 10f;
    public float damageCooldown = 1f;
    private float nextDamageTime = 0f;

    [Header("Target Detection")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius = 1f;
    [SerializeField] private LayerMask targetMask;   // Player layer

    private Entity_Stats stats;

    private void Awake()
    {
        stats = GetComponent<Entity_Stats>();
    }

    /// <summary>
    /// Gọi bằng Animation Event khi enemy đánh tới frame gây hit.
    /// </summary>
    public void PerformAttack()
    {
        if (Time.time < nextDamageTime) return;

        float finalDamage = stats != null ? stats.GetDamage() : damage;

        Collider2D[] targets = Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRadius,
            targetMask
        );

        foreach (var target in targets)
        {
            Entity_Health hp = target.GetComponent<Entity_Health>();

            if (hp != null && !hp.IsDead)
            {
                hp.TakeDamage(finalDamage);
                Debug.Log($"{name} đánh trúng {target.name}, gây damage: {finalDamage}");
            }
        }

        nextDamageTime = Time.time + damageCooldown;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            Debug.LogError("❌ AttackPoint của EnemyCombat chưa được gán!");
            return;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
