using UnityEngine;

public class BossRangedAttack : MonoBehaviour
{
    [Header("Bullet Pool (Manual)")]
    public GameObject[] bullets;    // Bạn tự kéo bullet vào đây

    [Header("Shoot Settings")]
    public Transform shootPoint;
    public float cooldown = 8f;

    private float nextShootTime = 0f;

    private BossTargetDetection detect;
    private BossAttackZone zone; // Zone mới

    void Awake()
    {
        detect = GetComponentInParent<BossTargetDetection>();
        zone = GetComponentInParent<BossAttackZone>();
    }

    // Gọi bằng Animation Event: Shoot()
    public void Shoot()
    {
        // Cooldown
        if (Time.time < nextShootTime) return;

        // Phải nhìn thấy player
        if (!detect.HasTarget) return;

        // 🔥 Quan trọng: phải trong DONUT ZONE mới bắn được
        if (!zone.playerInRangedZone) return;

        Transform player = detect.player;
        if (player == null) return;

        // Lấy bullet rảnh
        GameObject bullet = GetFreeBullet();
        if (bullet == null) return;

        // Đặt vị trí bắn
        bullet.transform.position = shootPoint.position;

        // Kích hoạt bullet
        bullet.SetActive(true);

        // Tính hướng bay
        Vector2 dir = (player.position - shootPoint.position).normalized;
        bullet.GetComponent<BossProjectile>().SetDirection(dir);

        // Reset cooldown
        nextShootTime = Time.time + cooldown;
    }

    // Tìm bullet chưa active
    private GameObject GetFreeBullet()
    {
        foreach (GameObject b in bullets)
        {
            if (!b.activeInHierarchy)
                return b;
        }
        return null; // hết đạn
    }
}
