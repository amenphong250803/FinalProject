using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damage = 10;
    public float attackRange = 1.5f;
    public float attackCooldown = 1f;
    public LayerMask playerLayer;

    private float lastAttackTime;

    private void Update()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, attackRange, playerLayer);

        if (player != null && Time.time - lastAttackTime > attackCooldown)
        {
            PlayerHealth hp = player.GetComponent<PlayerHealth>();
            if (hp != null)
            {
                hp.TakeDamage(damage);
                Debug.Log("💢 Enemy hit player!");
            }
            lastAttackTime = Time.time;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
