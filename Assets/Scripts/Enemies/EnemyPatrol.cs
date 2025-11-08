using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform leftPoint;
    public Transform rightPoint;
    public float speed = 2f;

    public float chaseRange = 4f;      // phạm vi phát hiện player
    private Transform player;

    private Rigidbody2D rb;
    private bool movingRight = true;
    private Vector3 originalScale;

    private EnemyHealth hp;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;

        hp = GetComponent<EnemyHealth>();

        leftPoint.parent = null;
        rightPoint.parent = null;

        originalScale = transform.localScale;

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (hp.currentHP <= 0)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= chaseRange)
            ChasePlayer();
        else
            Patrol();
    }

    void Patrol()
    {
        if (movingRight)
        {
            rb.linearVelocity = new Vector2(speed, 0);

            transform.localScale = new Vector3(
                Mathf.Abs(originalScale.x), originalScale.y, originalScale.z
            );

            if (transform.position.x >= rightPoint.position.x)
                movingRight = false;
        }
        else
        {
            rb.linearVelocity = new Vector2(-speed, 0);

            transform.localScale = new Vector3(
                -Mathf.Abs(originalScale.x), originalScale.y, originalScale.z
            );

            if (transform.position.x <= leftPoint.position.x)
                movingRight = true;
        }
    }

    void ChasePlayer()
    {
        if (player.position.x > transform.position.x)
        {
            rb.linearVelocity = new Vector2(speed * 1.2f, 0);
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
        else
        {
            rb.linearVelocity = new Vector2(-speed * 1.2f, 0);
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
    }
}
