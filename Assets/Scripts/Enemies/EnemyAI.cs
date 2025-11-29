using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed = 2f;
    public Transform leftPoint;
    public Transform rightPoint;
    private bool movingRight = true;

    [Header("Chase & Attack")]
    public float chaseRange = 4f;
    public float attackRange = 1.2f;
    public float attackCooldown = 1f;

    private float lastAttackTime = 0f;

    private Animator anim;
    private Transform player;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            Attack();
            return;
        }

        if (distance <= chaseRange)
        {
            Chase();
            return;
        }

        Patrol();
    }

    void Patrol()
    {
        anim.Play("Minotaur_WALK");

        if (movingRight)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            transform.localScale = new Vector3(1, 1, 1);

            if (transform.position.x > rightPoint.position.x)
                movingRight = false;
        }
        else
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            transform.localScale = new Vector3(-1, 1, 1);

            if (transform.position.x < leftPoint.position.x)
                movingRight = true;
        }
    }

    void Chase()
    {
        anim.Play("Minotaur_WALK");

        if (player.position.x > transform.position.x)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void Attack()
    {
        if (Time.time - lastAttackTime < attackCooldown) return;

        anim.Play("Minotaur_ATTACK");

        Collider2D hit = Physics2D.OverlapCircle(transform.position, attackRange, LayerMask.GetMask("Player"));
        if (hit)
        {
            Entity_Health hp = hit.GetComponent<Entity_Health>();
            if (hp != null)
                hp.TakeDamage(10);
        }

        lastAttackTime = Time.time;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
