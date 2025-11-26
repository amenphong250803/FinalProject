using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    public Transform leftPoint;
    public Transform rightPoint;

    [Header("Movement")]
    public float speed = 2f;
    public float idleTime = 2f;

    [Header("Model (only flip this)")]
    public Transform model;

    private Rigidbody2D rb;
    private Animator anim;

    private bool movingRight = true;
    private bool isIdle = false;
    private bool canMove = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        baseScale = new Vector3(0.8f, 0.8f, 1f);
        transform.localScale = baseScale;
    }

    private void Update()
    {
        if (!canMove || isIdle) return;
        PatrolMovement();
    }

    private void PatrolMovement()
    {
        anim.SetBool("walk", true);

        float leftX = Mathf.Min(leftPoint.position.x, rightPoint.position.x);
        float rightX = Mathf.Max(leftPoint.position.x, rightPoint.position.x);

        if (movingRight)
        {
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);

            if (transform.position.x >= rightX)
                StartCoroutine(IdleAtPoint(false));
        }
        else
        {
            rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);

            if (transform.position.x <= leftX)
                StartCoroutine(IdleAtPoint(true));
        }
    }

    private System.Collections.IEnumerator IdleAtPoint(bool nextDirectionRight)
    {
        isIdle = true;
        rb.linearVelocity = Vector2.zero;

        // Không PLAY state nữa — chỉ cần tắt walk là về idle
        anim.SetBool("walk", false);

        yield return new WaitForSeconds(idleTime);

        movingRight = nextDirectionRight;
        Flip(movingRight ? 1 : -1);

        isIdle = false;
    }

    private void Flip(int dir)
    {
        transform.localScale = new Vector3(
            baseScale.x * dir,
            baseScale.y,
            baseScale.z
        );
    }

    public void StopMoving()
    {
        canMove = false;
        rb.linearVelocity = Vector2.zero;
        anim.SetBool("walk", false);
    }
}
