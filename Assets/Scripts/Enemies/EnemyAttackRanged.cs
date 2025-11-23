using UnityEngine;

public class EnemyAttackRanged : MonoBehaviour
{
    [Header("Ranged Attack Settings")]
    public GameObject projectilePrefab;   // viên đạn
    public Transform shootPoint;          // nơi bắn ra
    public float shootForce = 7f;
    public float fireRate = 1.3f;

    private float lastFireTime = 0f;
    private bool playerInRange = false;

    private Transform player;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!playerInRange) return;

        // Player đã chết hoặc bị destroy
        if (player == null)
        {
            playerInRange = false;
            return;
        }

        // Bắn
        if (Time.time >= lastFireTime + fireRate)
        {
            Shoot();
            lastFireTime = Time.time;
        }
    }

    // Zone sẽ gọi hàm này
    public void SetPlayerInRange(bool isInRange, Transform target)
    {
        playerInRange = isInRange;
        player = target;
    }

    private void Shoot()
    {
        if (projectilePrefab == null || shootPoint == null) return;

        anim.SetTrigger("attack");

        // Spawn đạn
        GameObject bullet = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

        // Check nếu bullet bị destroy bất thường
        if (bullet == null) return;

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // bay theo hướng player
            Vector2 dir = (player.position - shootPoint.position).normalized;
            rb.linearVelocity = dir * shootForce;
        }

        // Xoay enemy theo hướng player
        if (player.position.x > transform.position.x)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);
    }
}
