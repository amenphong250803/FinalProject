using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float runSpeed = 7f;
    private float currentSpeed;

    [Header("Jump")]
    public float jumpForce = 12f;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;
    private bool isGrounded;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    private float horizontal;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        // Check ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        // Run or walk
        currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : moveSpeed;

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // Attack
        if (Input.GetKeyDown(KeyCode.J))
        {
            anim.SetTrigger("Attack");
        }

        // Flip sprite
        if (horizontal > 0) sr.flipX = false;
        if (horizontal < 0) sr.flipX = true;

        // Animator
        anim.SetFloat("Speed", Mathf.Abs(horizontal));
        anim.SetBool("IsGrounded", isGrounded);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * currentSpeed, rb.linearVelocity.y);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
        }
    }
}
