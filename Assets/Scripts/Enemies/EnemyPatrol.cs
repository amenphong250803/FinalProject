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

    private Vector3 baseScale;

    private bool movingRight = true;
    private bool isIdle = false;
    public bool canMove = true;

    private int defaultDirection = 1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (model != null)
            anim = model.GetComponent<Animator>();


        baseScale = model != null ? model.localScale : new Vector3(1, 1, 1);

        defaultDirection = baseScale.x >= 0 ? 1 : -1;
    }

    private void Update()
    {
        if (!canMove || isIdle) return;
        PatrolMovement();
    }

    private void PatrolMovement()
    {
        anim?.SetBool("walk", true);

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

        anim?.SetBool("walk", false);

        yield return new WaitForSeconds(idleTime);

        movingRight = nextDirectionRight;
        Flip(movingRight ? 1 : -1);

        isIdle = false;
    }

    public void Flip(int dir)
    {
        if (model != null)
        {
            model.localScale = new Vector3(
                Mathf.Abs(baseScale.x) * dir,
                baseScale.y,
                baseScale.z
            );
        }
    }

    public void FaceDefaultDirection()
    {
        Flip(defaultDirection);
    }

    public void FacePatrolDirection()
    {
        Flip(movingRight ? 1 : -1);
    }

    public void StopMoving()
    {
        canMove = false;
        rb.linearVelocity = Vector2.zero;
        anim?.SetBool("walk", false);
    }

    public void ResumePatrol()
    {
        canMove = true;
        isIdle = false;
        anim?.SetBool("walk", true);

        FacePatrolDirection();
    }

    public void StopPatrol()
    {
        canMove = false;
        rb.linearVelocity = Vector2.zero;
        anim?.SetBool("walk", false);
    }
}
