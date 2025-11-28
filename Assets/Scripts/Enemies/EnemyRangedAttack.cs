using UnityEngine;

public class EnemyRangedAttack : MonoBehaviour
{
    [Header("Bullet Pool")]
    public GameObject[] bullets;

    [Header("Shoot Settings")]
    public Transform shootPoint;
    public float cooldown = 3f;

    [Header("Shoot Sound")]
    public AudioClip shootSFX;
    public AudioSource audioSource;

    private float nextShootTime;

    private EnemyTargetDetection detect;
    private EnemyRangedZone zone;
    private EnemyPatrol patrol;
    private Animator anim;
    private EnemyHealth hp;

    private bool wasInZone = false;

    void Awake()
    {
        detect = GetComponentInParent<EnemyTargetDetection>();
        zone = GetComponentInChildren<EnemyRangedZone>();
        patrol = GetComponentInParent<EnemyPatrol>();
        anim = GetComponentInChildren<Animator>();
        hp = GetComponentInParent<EnemyHealth>();

        if (audioSource == null)
            audioSource = GetComponentInParent<AudioSource>();
    }

    void Update()
    {
        if (hp != null && hp.IsDead)
        {
            anim.ResetTrigger("attack");
            return;
        }

        if (!detect || !zone) return;

        // ❌ PLAYER RỜI ZONE → trở lại patrol
        if (!zone.playerInRangedZone)
        {
            if (wasInZone)
            {
                StopAttackAndResumePatrol();
                wasInZone = false;
            }
            return;
        }

        // ✔ PLAYER TRONG ZONE
        wasInZone = true;

        // ⭐ 1. DỪNG DI CHUYỂN NGAY LẬP TỨC
        if (patrol != null)
            patrol.StopPatrol();

        // ⭐ 2. QUAY VỀ HƯỚNG PLAYER
        FlipTowardPlayer();

        if (!detect.HasTarget) return;
        if (Time.time < nextShootTime) return;

        // ⭐ 3. BẮN ĐẠN
        anim.SetTrigger("attack");
        anim.SetBool("attack", true);

        // đứng im trong lúc bắn → patrol đã stop
        nextShootTime = Time.time + cooldown;
    }

    // =====================================================================
    // ⭐ PLAYER RỜI ZONE → quay về patrol bình thường
    // =====================================================================
    private void StopAttackAndResumePatrol()
    {
        anim.ResetTrigger("attack");
        anim.SetBool("attack", false);

        if (patrol != null)
            patrol.ResumePatrol();
    }

    // =====================================================================
    // ⭐ QUAY HƯỚNG THEO PLAYER
    // =====================================================================
    private void FlipTowardPlayer()
    {
        if (!detect.HasTarget || patrol == null) return;

        Transform player = detect.player;
        if (player == null) return;

        bool right = player.position.x > transform.position.x;
        patrol.Flip(right ? 1 : -1);
    }

    // =====================================================================
    // ⭐ Animation Event gọi hàm này để bắn đạn
    // =====================================================================
    public void Shoot()
    {
        // NGĂN BẮN KHI PLAYER RA KHỎI ZONE
        if (!zone.playerInRangedZone) return;

        if (!detect.HasTarget) return;
        if (hp != null && hp.IsDead) return;

        if (shootSFX != null && audioSource != null)
            audioSource.PlayOneShot(shootSFX);

        Transform player = detect.player;
        if (player == null) return;

        GameObject bullet = GetFreeBullet();
        if (bullet == null) return;

        bullet.transform.position = shootPoint.position;
        bullet.SetActive(true);

        Vector2 dir = (player.position - shootPoint.position).normalized;

        EnemyProjectile proj = bullet.GetComponent<EnemyProjectile>();
        if (proj != null)
            proj.SetDirection(dir);
    }


    private GameObject GetFreeBullet()
    {
        foreach (GameObject b in bullets)
            if (!b.activeInHierarchy)
                return b;
        return null;
    }
}
