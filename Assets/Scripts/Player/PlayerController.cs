using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f; // Tốc độ chạy
    public float jumpForce = 10f; // Lực nhảy
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Debug components
        if (rb == null) Debug.LogError("Rigidbody2D missing!");
        if (animator == null) Debug.LogError("Animator missing!");
        if (spriteRenderer == null) Debug.LogError("SpriteRenderer missing!");
    }

    void Update()
    {
        // Di chuyển
        float moveInput = Input.GetAxisRaw("Horizontal");
        HandleMovement(moveInput);

        // Flip sprite
        if (moveInput > 0)
            spriteRenderer.flipX = false;
        else if (moveInput < 0)
            spriteRenderer.flipX = true;

        // Animation
        animator.SetBool("isRunning", moveInput != 0);
        animator.SetBool("isJumping", !isGrounded); // Trigger nhảy khi không chạm đất

        // Nhảy
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Debug.Log("Jump triggered!");
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
        }

        // Debug trạng thái
        Debug.Log("isGrounded: " + isGrounded + ", Velocity Y: " + rb.linearVelocity.y);
    }

    private void HandleMovement(float moveInput)
    {
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            Debug.Log("Landed on Ground");
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            Debug.Log("Left Ground");
        }
    }
}