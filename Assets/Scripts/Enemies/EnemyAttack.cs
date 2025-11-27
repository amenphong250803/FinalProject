using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public float attackCooldown = 1f;
    public float attackRange = 1.5f;
    public int damage = 10;

    private float lastAttackTime = 0f;

    [Header("Target")]
    public Transform target;

    private Animator anim;
    private EnemyHealth enemyHp;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        enemyHp = GetComponent<EnemyHealth>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            target = playerObj.transform;
    }

    private void Update()
    {
        if (enemyHp != null && enemyHp.IsDead) return;
        if (target == null) return;

        float distance = Vector2.Distance(transform.position, target.position);

        // Nếu player gần đủ tầm đánh
        if (distance <= attackRange)
        {
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                anim.SetTrigger("attack");
                lastAttackTime = Time.time;
            }
        }
    }

    /// <summary>
    /// Damage được gọi đúng lúc bởi Animation Event
    /// </summary>
    public void DealDamage()
    {
        if (target == null) return;

        float distance = Vector2.Distance(transform.position, target.position);

        if (distance <= attackRange)
        {
            Entity_Health hp = target.GetComponent<Entity_Health>();
            if (hp != null && !hp.IsDead)
            {
                hp.TakeDamage(damage);
                Debug.Log($"{name} hit {target.name} for {damage} dmg");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
