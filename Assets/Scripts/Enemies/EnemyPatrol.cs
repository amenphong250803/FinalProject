using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float groundCheckDistance = 1f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;

    private bool movingLeft = true;
    private Animator anim;
    private bool isDead;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isDead) return;
        Patrol();
    }

    private void Patrol()
    {
        anim.SetBool("Moving", true);

        // 🟢 Dịch chuyển theo hướng local (không bị cố định hướng)
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime, Space.Self);

        // Kiểm tra mặt đất
        RaycastHit2D groundInfo = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);

        if (groundInfo.collider == false)
        {
            Flip();
        }
    }

    private void Flip()
    {
        movingLeft = !movingLeft;

        if (movingLeft)
            transform.eulerAngles = new Vector3(0, 0, 0);
        else
            transform.eulerAngles = new Vector3(0, 180, 0);
    }

    public void TakeHit()
    {
        if (isDead) return;
        anim.SetTrigger("Hurt");
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        anim.SetBool("Moving", false);
        anim.SetBool("Die", true);
        Destroy(gameObject, 2f);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
    }
}
