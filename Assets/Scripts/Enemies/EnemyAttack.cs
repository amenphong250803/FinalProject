using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 1f;
    public int damage = 10;
    public float cooldown = 1f;
    public LayerMask playerLayer;

    private float lastAttackTime;
    private EnemyHealth health;
    private Animator anim;

    private void Awake()
    {
        health = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (health.isDead) return;           // CHẶN ATTACK KHI CHẾT

        Collider2D player = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);

        if (player != null && Time.time >= lastAttackTime + cooldown)
        {
            anim.SetTrigger("attack");

            PlayerHealth p = player.GetComponent<PlayerHealth>();
            if (p != null)
                p.TakeDamage(damage);

            lastAttackTime = Time.time;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
