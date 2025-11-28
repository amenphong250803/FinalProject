using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [Header("Combat Settings")]
    public float damage = 10f;
    public float attackCooldown = 1f;
    private float nextAttackTime = 0f;

    [Header("Chase Settings")]
    public float chaseRange = 6f;
    public float stopChaseRange = 9f;
    public float chaseSpeed = 2.5f;

    [Header("Attack Range")]
    public Transform attackPoint;
    public float attackRadius = 1f;
    public LayerMask targetMask;

    [Header("SFX")]
    public AudioSource audioSource;        // Nơi phát âm thanh (gắn vào Model hoặc Enemy)        
    public AudioClip hitSFX;               // Âm thanh khi đánh trúng Player

    private Transform target;
    private Entity_Stats stats;
    private EnemyPatrol patrol;
    private Animator anim;
    private Rigidbody2D rb;

    private bool isChasing = false;
    private bool canAttackPlayer = false;

    private void Awake()
    {
        stats = GetComponent<Entity_Stats>();
        patrol = GetComponent<EnemyPatrol>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) target = p.transform;
    }

    private void Update()
    {
        if (target == null) return;

        float distance = Vector2.Distance(transform.position, target.position);

        // 1) Bắt đầu chase
        if (!isChasing && distance <= chaseRange)
            StartChasing();

        // 2) Nếu đang chase
        if (isChasing)
        {
            // Player chạy quá xa → quay về patrol
            if (distance >= stopChaseRange)
            {
                StopChasing();
                return;
            }

            // Nếu chưa vào tầm attack → đuổi theo
            if (distance > attackRadius + 0.2f)
            {
                MoveTowardPlayer();
            }
            else
            {
                TryAttack();
            }
        }
    }

    // ======================================================
    // ⭐ BẮT ĐẦU CHASE
    // ======================================================
    private void StartChasing()
    {
        isChasing = true;
        patrol.StopPatrol();              // Tắt patrol đúng chuẩn của bạn
        anim?.SetBool("walk", true);
    }

    // ======================================================
    // ⭐ NGỪNG CHASE – QUAY LẠI PATROL
    // ======================================================
    private void StopChasing()
    {
        isChasing = false;

        rb.linearVelocity = Vector2.zero;
        anim?.SetBool("walk", false);

        patrol.ResumePatrol();            // Tiếp tục patrol theo đúng hướng ban đầu
        patrol.FacePatrolDirection();     // Quay về hướng patrol

        Debug.Log($"{name} quay lại patrol");
    }

    // ======================================================
    // ⭐ DÍ THEO PLAYER (không phá logic EnemyPatrol)
    // ======================================================
    private void MoveTowardPlayer()
    {
        Vector2 dir = (target.position - transform.position).normalized;

        rb.linearVelocity = new Vector2(dir.x * chaseSpeed, rb.linearVelocity.y);

        // flip model
        patrol.Flip(dir.x > 0 ? 1 : -1);

        anim?.SetBool("walk", true);
    }

    // ======================================================
    // ⭐ GỌI Animation Attack
    // ======================================================
    private void TryAttack()
    {
        rb.linearVelocity = Vector2.zero;

        if (Time.time >= nextAttackTime)
        {
            anim.SetTrigger("attack");
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    // ======================================================
    // ⭐ DAMAGE – gọi bởi Animation Event
    // ======================================================
    public void PerformAttack()
    {
        float finalDamage = stats != null ? stats.GetDamage() : damage;

        Collider2D[] targets = Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRadius,
            targetMask
        );

        bool hitSomeone = false;

        foreach (Collider2D t in targets)
        {
            Entity_Health hp = t.GetComponent<Entity_Health>();
            if (hp != null && !hp.IsDead)
            {
                hp.TakeDamage(finalDamage);
                hitSomeone = true;

                Debug.Log($"{name} đánh {t.name} gây {finalDamage} damage");
            }
        }

        // ⭐ SFX: âm đánh trúng
        if (hitSomeone && audioSource != null && hitSFX != null)
            audioSource.PlayOneShot(hitSFX);
    }


    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, stopChaseRange);
    }
}
