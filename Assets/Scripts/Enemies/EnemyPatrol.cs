using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    public Transform leftPoint;
    public Transform rightPoint;

    [Header("Movement")]
    public float speed = 1f;

    private Rigidbody2D rb;
    private Animator anim;
    private Vector3 baseScale;

    private bool movingRight = true;
    private bool canMove = true;         // NGĂN di chuyển khi chết

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        baseScale = transform.localScale; // scale gốc
    }

    private void Update()
    {
        if (!canMove) return;
        PatrolMovement();
    }

    private void PatrolMovement()
    {
        if (anim != null)
            anim.SetBool("walk", true); // phải tồn tại trong Animator

        float leftX = Mathf.Min(leftPoint.position.x, rightPoint.position.x);
        float rightX = Mathf.Max(leftPoint.position.x, rightPoint.position.x);

        // ➤ DI CHUYỂN SANG PHẢI
        if (movingRight)
        {
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);

            if (transform.position.x >= rightX)
            {
                movingRight = false;
                Flip(-1);
            }
        }
        // ➤ DI CHUYỂN SANG TRÁI
        else
        {
            rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);

            if (transform.position.x <= leftX)
            {
                movingRight = true;
                Flip(1);
            }
        }
    }

    private void Flip(int dir)
    {
        transform.localScale = new Vector3(Mathf.Abs(baseScale.x) * dir, baseScale.y, baseScale.z);
    }

    // ➤ HÀM NÀY 100% NGĂN enemy DI CHUYỂN
    public void StopMoving()
    {
        canMove = false;
        rb.linearVelocity = Vector2.zero;

        if (anim != null)
            anim.SetBool("walk", false);
    }
}
