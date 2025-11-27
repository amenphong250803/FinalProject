using UnityEngine;

public class EnemyRangedAttack : MonoBehaviour
{
    [Header("Bullet Pool")]
    public GameObject[] bullets;

    [Header("Shoot Settings")]
    public Transform shootPoint;
    public float cooldown = 3f;

    private float nextShootTime;

    private EnemyTargetDetection detect;
    private EnemyRangedZone zone;
    private EnemyPatrol patrol;
    private Animator anim;
    private EnemyHealth hp;

    // để tránh reset patrol liên tục
    private bool wasInZone = false;

    void Awake()
    {
        detect = GetComponentInParent<EnemyTargetDetection>();
        zone = GetComponentInChildren<EnemyRangedZone>();

        // ⭐ EnemyRangedAttack luôn nằm trong child → phải lấy patrol ở cha
        patrol = GetComponentInParent<EnemyPatrol>();

        anim = GetComponentInChildren<Animator>();

        // ⭐ Health cũng nằm ở cha
        hp = GetComponentInParent<EnemyHealth>();
    }

    void Update()
    {
        // =============================================================
        // ⭐ 1. NGĂN MỌI HÀNH ĐỘNG SAU KHI CHẾT
        // =============================================================
        if (hp != null && hp.IsDead)
        {
            anim.ResetTrigger("attack");
            return;
        }

        if (!detect || !zone) return;

        // =============================================================
        // ⭐ 2. PLAYER KHÔNG Ở TRONG ZONE → TRỞ LẠI PATROL (CHỈ 1 LẦN)
        // =============================================================
        if (!zone.playerInRangedZone)
        {
            if (wasInZone)
            {
                StopAttackAndResumePatrol();
                wasInZone = false;
            }
            return;
        }

        // từ đây trở đi nghĩa là Player đang trong zone
        wasInZone = true;

        // =============================================================
        // ⭐ 3. QUAY MẶT THEO PLAYER (flip model)
        // =============================================================
        FlipTowardPlayer();

        // =============================================================
        // ⭐ 4. BẮN ĐẠN NẾU ĐƯỢC PHÉP
        // =============================================================
        if (!detect.HasTarget) return;
        if (Time.time < nextShootTime) return;

        anim.SetTrigger("attack");
        nextShootTime = Time.time + cooldown;
    }

    // =====================================================================
    // ⭐ HÀM: Player ra khỏi zone → enemy quay về patrol & hướng ban đầu
    // =====================================================================
    private void StopAttackAndResumePatrol()
    {
        anim.ResetTrigger("attack");
        anim.SetBool("attack", false);

        if (patrol != null)
        {
            patrol.ResumePatrol();        // tiếp tục đi tuần
        }
    }

    // =====================================================================
    // ⭐ HÀM: Quay enemy theo hướng player
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
    // ⭐ Enemy animation dùng event Shoot()
    // =====================================================================
    public void Shoot()
    {
        if (!detect.HasTarget) return;
        if (hp != null && hp.IsDead) return;

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
