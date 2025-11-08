using UnityEngine;

namespace ClearSky
{
    public class SimplePlayerController : MonoBehaviour
    {
        [Header("Movement")]
        public float movePower = 10f;
        public float jumpPower = 15f; // Set gravity scale = 5

        [Header("Ground Check")]
        public Transform groundCheck;
        public float groundRadius = 0.2f;
        public LayerMask groundLayer;
        private bool isGrounded;

        private Rigidbody2D rb;
        private Animator anim;

        private bool isJumping = false;
        private bool alive = true;
        private Vector3 originalScale;
        private int direction = 1;
        private float horizontal;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();

            originalScale = transform.localScale;

            // Khóa xoay để không bị đổ nghiêng
            rb.freezeRotation = true;
        }

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
        //                      GROUND CHECK
        // ============================================================
        void UpdateAnimations()
        {
            // Kiểm tra chạm đất bằng OverlapCircle
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

            // isJump = true nếu KHÔNG chạm đất
            anim.SetBool("isJump", !isGrounded);

            // isRun = true khi đang chạy trên mặt đất
            anim.SetBool("isRun", isGrounded && Mathf.Abs(horizontal) > 0.1f);
        }

        // ============================================================
        //                           RUN
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

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);

            isJumping = false;
        }

        // ============================================================
        //                           ATTACK
        // ============================================================
        void Attack()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                anim.SetTrigger("attack");
            }
        }

        // ============================================================
        //                           HURT
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
        //                            DIE
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
        //                          RESTART
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
        //                     DEBUG DRAW (GROUND CHECK)
        // ============================================================
        private void OnDrawGizmosSelected()
        {
            if (groundCheck != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
            }
        }
    }
}
