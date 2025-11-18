using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackCooldown = 1f;
    public int damage = 10;

    [HideInInspector] public bool canAttack = false;
    [HideInInspector] public Transform target;

    private float lastAttack;
    private Animator anim;
    private EnemyHealth hp;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        hp = GetComponent<EnemyHealth>();
    }

    private void Update()
    {
        if (hp.IsDead) return;
        if (!canAttack) return;

        // đánh nếu đến cooldown
        if (Time.time >= lastAttack + attackCooldown)
        {
            anim.SetTrigger("attack");

            PlayerHealth p = target.GetComponent<PlayerHealth>();
            if (p != null)
            {
                p.TakeDamage(damage);
            }

            lastAttack = Time.time;
        }
    }
}
