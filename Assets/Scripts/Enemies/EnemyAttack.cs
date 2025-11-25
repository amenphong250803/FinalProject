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

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            target = playerObj.transform;
        }
    }

    private void Update()
    {
        if (hp.IsDead) return;
        if (!canAttack) return;

        if (target == null) return;

        // đánh nếu đến cooldown
        if (Time.time >= lastAttack + attackCooldown)
        {
            anim.SetTrigger("attack");

            Entity_Health p = target.GetComponent<Entity_Health>();
            if (p != null)
            {
                p.TakeDamage(damage);
            }

            lastAttack = Time.time;
        }
    }
}
