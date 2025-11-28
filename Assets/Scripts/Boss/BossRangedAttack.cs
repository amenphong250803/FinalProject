using UnityEngine;

public class BossRangedAttack : MonoBehaviour
{
    [Header("Bullet Pool (Manual)")]
    public GameObject[] bullets;

    [Header("Shoot Settings")]
    public Transform shootPoint;
    public float cooldown = 8f;

    private float nextShootTime = 0f;

    private BossTargetDetection detect;
    private BossAttackZone zone;

    [Header("Audio")]
    public AudioSource audioSource;    // ⭐ GẮN AudioSource
    public AudioClip shootSFX;         // ⭐ Âm thanh bắn đạn

    void Awake()
    {
        detect = GetComponentInParent<BossTargetDetection>();
        zone = GetComponentInParent<BossAttackZone>();
    }

    // Gọi bằng Animation Event
    public void Shoot()
    {
        // Cooldown
        if (Time.time < nextShootTime) return;

        // Phải nhìn thấy player
        if (!detect.HasTarget) return;

        // Phải ở trong vùng DONUT ZONE
        if (!zone.playerInRangedZone) return;

        Transform player = detect.player;
        if (player == null) return;

        // Lấy bullet còn trống
        GameObject bullet = GetFreeBullet();
        if (bullet == null) return;

        // Đặt vị trí bullet
        bullet.transform.position = shootPoint.position;

        // Kích hoạt bullet
        bullet.SetActive(true);

        // Tính hướng bay
        Vector2 dir = (player.position - shootPoint.position).normalized;
        bullet.GetComponent<BossProjectile>().SetDirection(dir);

        // 🔊 PLAY SFX
        PlayShootSFX();

        // Reset timer
        nextShootTime = Time.time + cooldown;
    }

    private GameObject GetFreeBullet()
    {
        foreach (GameObject b in bullets)
        {
            if (!b.activeInHierarchy)
                return b;
        }
        return null;
    }

    // ===========================
    //        🔊 SFX BẮN
    // ===========================
    private void PlayShootSFX()
    {
        if (audioSource == null || shootSFX == null)
            return;

        audioSource.PlayOneShot(shootSFX);
    }
}
