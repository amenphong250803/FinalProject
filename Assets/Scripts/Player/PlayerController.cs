using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 20f; // Tốc độ chạy
    public float jumpForce = 10f; // Lực nhảy
    private Rigidbody2D rb;
    private Animator animator; // Để control animation
    private bool isGrounded;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Lấy input di chuyển
        float moveInput = Input.GetAxisRaw("Horizontal"); // A/D hoặc Left/Right arrow
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        // Flip sprite khi di chuyển trái/phải
        if (moveInput > 0)
            spriteRenderer.flipX = false;
        else if (moveInput < 0)
            spriteRenderer.flipX = true;

        // Cập nhật animation dựa trên di chuyển
        animator.SetBool("isRunning", moveInput != 0); // isRunning = true nếu có input, false nếu đứng yên

        // Nhảy
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}