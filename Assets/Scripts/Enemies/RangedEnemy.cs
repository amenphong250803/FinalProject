using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    //[Header("Detection Zone")]
    //public float detectionRadius = 5f;
    //public LayerMask playerMask;

    //[Header("Attack Settings")]
    //public float attackCooldown = 2f;
    //public GameObject projectilePrefab;
    //public Transform shootPoint;

    //private Transform target;
    //private float lastShootTime = 0f;

    //private Animator anim;
    //private bool isDead = false;

    //private void Awake()
    //{
    //    anim = GetComponentInChildren<Animator>();
    //}

    //private void Update()
    //{
    //    if (isDead) return;  // ⭐ enemy đã chết → dừng AI

    //    DetectPlayer();

    //    if (target != null)
    //    {
    //        FlipTowardsPlayer();
    //        TryShoot();
    //    }
    //}

    //private void DetectPlayer()
    //{
    //    if (isDead) return;

    //    Collider2D hit = Physics2D.OverlapCircle(transform.position, detectionRadius, playerMask);
    //    target = hit ? hit.transform : null;
    //}

    //private void TryShoot()
    //{
    //    if (isDead) return;

    //    if (Time.time < lastShootTime + attackCooldown) return;
    //    if (target == null) return;

    //    lastShootTime = Time.time;
    //    anim.SetTrigger("attack");
    //}

    //// Animation Event
    //public void ShootProjectile()
    //{
    //    if (isDead) return;  // ⭐ animation attack bị gọi khi enemy chết → không bắn

    //    if (projectilePrefab == null || shootPoint == null || target == null) return;

    //    GameObject bullet = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

    //    EnemyProjectile proj = bullet.GetComponent<EnemyProjectile>();
    //    if (proj != null)
    //    {
    //        Vector2 dir = (target.position - shootPoint.position).normalized;
    //        proj.Setup(dir);
    //    }
    //}

    //private void FlipTowardsPlayer()
    //{
    //    if (target == null || isDead) return;

    //    transform.localScale =
    //        (target.position.x > transform.position.x)
    //        ? new Vector3(1, 1, 1)
    //        : new Vector3(-1, 1, 1);
    //}

    //// ⭐ Gọi hàm này khi máu enemy = 0
    //public void Die()
    //{
    //    if (isDead) return;

    //    isDead = true;

    //    if (anim != null)
    //        anim.SetTrigger("dead");

    //    // tắt collider
    //    Collider2D col = GetComponent<Collider2D>();
    //    if (col != null) col.enabled = false;

    //    // tắt rigidbody
    //    Rigidbody2D rb = GetComponent<Rigidbody2D>();
    //    if (rb != null)
    //    {
    //        rb.linearVelocity = Vector2.zero;
    //        rb.isKinematic = true;
    //    }
    //}

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.cyan;
    //    Gizmos.DrawWireSphere(transform.position, detectionRadius);
    //}
}
