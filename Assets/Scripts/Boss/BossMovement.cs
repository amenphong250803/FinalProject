using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;
    public float stopDistance = 3f;
    public float patrolRadius = 10f;

    [Header("Detection")]
    public float detectionRadius = 8f;
    public bool playerInRange = false;

    private BossTargetDetection detect;
    private Rigidbody2D rb;
    private BossHealth hp;
    private Animator anim;

    private Vector3 startPosition;
    private int facing = 1;

    void Awake()
    {
        detect = GetComponent<BossTargetDetection>();
        rb = GetComponent<Rigidbody2D>();
        hp = GetComponent<BossHealth>();
        anim = GetComponentInChildren<Animator>();

        startPosition = transform.position;
    }

    void FixedUpdate()
    {
        if (hp.IsDead)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        if (!detect.HasTarget)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        float distToPlayer = detect.distanceToPlayer;
        playerInRange = distToPlayer <= detectionRadius;

        if (!playerInRange)
        {
            MoveBackToSpawn();
            return;
        }

        MoveTowardPlayer(distToPlayer);
    }

    void MoveTowardPlayer(float distToPlayer)
    {
        if (distToPlayer > stopDistance)
        {
            Vector2 direction = (detect.player.position - transform.position).normalized;

            if (Vector2.Distance(startPosition, transform.position) < patrolRadius)
                rb.linearVelocity = direction * moveSpeed;
            else
                MoveBackToSpawn();
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }

        Flip();
    }

    void MoveBackToSpawn()
    {
        Vector2 direction = (startPosition - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;

        Flip();
    }

    void Flip()
    {
        if (detect.player == null) return;

        float dirX = detect.player.position.x - transform.position.x;

        if (dirX > 0 && facing != 1)
        {
            facing = 1;
            anim.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (dirX < 0 && facing != -1)
        {
            facing = -1;
            anim.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
