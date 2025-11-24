using UnityEngine;

public class EnemyAttackRanged : MonoBehaviour
{
    [Header("Attack Settings")]
    public Transform shotPoint;           // chỗ bắn bullet
    public GameObject[] bulletPool;      // mảng bullet đã đặt sẵn trong scene
    public float attackCooldown = 1f;    // thời gian giữa 2 lần bắn

    private float lastShootTime;
    private Transform target;            // player trong tầm
    private Animator anim;
    private EnemyHealth health;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        health = GetComponent<EnemyHealth>();
    }

    /// <summary>
    /// Gọi từ EnemyRangedZone khi player vào/ra attack zone
    /// </summary>
    public void SetPlayerInRange(bool inRange, Transform player)
    {
        target = inRange ? player : null;
    }

    private void Update()
    {
        // Chết rồi thì không bắn nữa
        //if (health != null && health.IsDead) return;

        // Không có player trong tầm thì không làm gì
        if (target == null) return;

        // Đủ cooldown thì cho phép bắn (gọi animation attack)
        if (Time.time >= lastShootTime + attackCooldown)
        {
            // Gọi animation Attack – animation sẽ gọi event Shoot()
            if (anim != null)
            {
                anim.SetTrigger("attack");
            }

            lastShootTime = Time.time;
        }
    }

    /// <summary>
    /// HÀM NÀY GỌI BẰNG ANIMATION EVENT trong Minator2 attack
    /// </summary>
    public void Shoot()
    {
        if (shotPoint == null)
        {
            Debug.LogWarning("❌ shotPoint chưa gán trên EnemyAttackRanged");
            return;
        }

        if (target == null)
        {
            // Player đã đi khỏi zone đúng lúc animation tới frame bắn
            return;
        }

        GameObject bullet = GetBulletFromPool();
        if (bullet == null)
        {
            Debug.LogWarning("⚠ Bullet Pool hết đạn!");
            return;
        }

        // Đặt vị trí & xoay cho bullet
        bullet.transform.position = shotPoint.position;
        bullet.transform.rotation = shotPoint.rotation;

        // Tính hướng bắn theo vị trí player (trái / phải)
        float dirX = Mathf.Sign(target.position.x - transform.position.x);
        Vector2 dir = new Vector2(dirX, 0f);

        Bullet bulletComp = bullet.GetComponent<Bullet>();
        if (bulletComp != null)
        {
            bulletComp.SetDirection(dir);
        }

        bullet.SetActive(true);
    }

    /// <summary>
    /// Lấy 1 viên bullet chưa active trong pool
    /// </summary>
    private GameObject GetBulletFromPool()
    {
        foreach (GameObject b in bulletPool)
        {
            if (b != null && !b.activeInHierarchy)
            {
                return b;
            }
        }

        return null; // hết đạn thì return null
    }
}
