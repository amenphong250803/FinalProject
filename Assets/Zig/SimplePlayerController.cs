using UnityEngine;
public class SimplePlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float movePower = 6f;
    public float jumpPower = 12f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;
    private bool isGrounded;

    [Header("Attack")]
    public GameObject hitbox;     // <-- QUAN TRỌNG: phải gán object này trong Inspector

    private Rigidbody2D rb;
    private Animator anim;

    private float horizontal;
    private bool isJumping = false;
    private bool alive = true;

    private Vector3 originalScale;
    private int direction = 1;


    // ============================================================
    //                          START
    // ============================================================
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        originalScale = transform.localScale;

        // Không cho xoay
        rb.freezeRotation = true;

        // Tắt hitbox lúc đầu
        if (hitbox != null)
            hitbox.SetActive(false);
    }


    // ============================================================
    //                          UPDATE
    // ============================================================
    void Update()
    {
        Restart();
        if (!alive) return;

        Hurt();
        Die();
        Attack();
        Jump();
        Run();

        UpdateAnimations();
    }


    // ============================================================
    //                       UPDATE ANIMATIONS
    // ============================================================
    void UpdateAnimations()
    {
        // Kiểm tra mặt đất
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
        anim.SetBool("isJump", !isGrounded);

        // Run animation
        anim.SetBool("isRun", isGrounded && Mathf.Abs(horizontal) > 0.1f);
    }


    // ============================================================
    //                            RUN
    // ============================================================
    void Run()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        rb.linearVelocity = new Vector2(horizontal * movePower, rb.linearVelocity.y);

        if (horizontal > 0)
        {
            direction = 1;
            Flip();
        }
        else if (horizontal < 0)
        {
            direction = -1;
            Flip();
        }
    }

    void Flip()
    {
        transform.localScale = new Vector3(
            originalScale.x * direction,
            originalScale.y,
            originalScale.z
        );
    }


    // ============================================================
    //                           JUMP
    // ============================================================
    void Jump()
    {
        if ((Input.GetButtonDown("Jump") || Input.GetAxisRaw("Vertical") > 0) && isGrounded)
        {
            isJumping = true;
        }

        if (!isJumping) return;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);

        isJumping = false;
    }


    // ============================================================
    //                 ATTACK (CLICK CHUỘT TRÁI)
    // ============================================================
    void Attack()
    {
        if (Input.GetMouseButtonDown(0)) // Mouse Left Click
        {
            anim.SetTrigger("attack");
        }
    }

    // ===== Animation Events =====
    public void EnableHitbox()
    {
        if (hitbox != null)
            hitbox.SetActive(true);
    }

    public void DisableHitbox()
    {
        if (hitbox != null)
            hitbox.SetActive(false);
    }


    // ============================================================
    //                            HURT
    // ============================================================
    void Hurt()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            anim.SetTrigger("hurt");

            float knock = direction == 1 ? -5f : 5f;
            rb.AddForce(new Vector2(knock, 1f), ForceMode2D.Impulse);
        }
    }


    // ============================================================
    //                             DIE
    // ============================================================
    void Die()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            anim.SetTrigger("die");
            alive = false;
            rb.linearVelocity = Vector2.zero;
        }
    }


    // ============================================================
    //                           RESTART
    // ============================================================
    void Restart()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            anim.SetTrigger("idle");
            alive = true;
        }
    }


    // ============================================================
    //                DEBUG (GROUND CHECK VISUAL)
    // ============================================================
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
        }
    }
}