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

    private EnemyHealth hp;   // ⭐ LẤY COMPONENT HEALTH
    private int defaultDirection;

    void Awake()
    {
        detect = GetComponentInParent<EnemyTargetDetection>();
        zone = GetComponentInChildren<EnemyRangedZone>();
        patrol = GetComponent<EnemyPatrol>();

        anim = GetComponentInChildren<Animator>();
        hp = GetComponentInParent<EnemyHealth>();  // ⭐ LẤY HEALTH

        defaultDirection = transform.localScale.x >= 0 ? 1 : -1;
    }

    void Update()
    {
        // ⭐ NGĂN MỌI HÀNH VI SAU KHI CHẾT
        if (hp != null && hp.IsDead)
        {
            anim.ResetTrigger("attack");
            return;
        }

        if (!detect || !zone) return;

        // PLAYER RỜI ZONE
        if (!zone.playerInRangedZone)
        {
            StopAttackAndReset();
            return;
        }

        // PLAYER TRONG ZONE
        FlipToPlayer();

        if (!detect.HasTarget) return;
        if (Time.time < nextShootTime) return;

        anim.SetTrigger("attack");
        nextShootTime = Time.time + cooldown;
    }

    // STOP ATTACK + RESET
    private void StopAttackAndReset()
    {
        anim.ResetTrigger("attack");
        anim.SetBool("attack", false);
        anim.Play("Idle");

        if (patrol != null)
        {
            patrol.FaceDefaultDirection();
            patrol.ResumePatrol();
        }
    }

    // FLIP THEO PLAYER
    private void FlipToPlayer()
    {
        if (!detect.HasTarget || patrol == null) return;

        Transform player = detect.player;
        if (player == null) return;

        bool playerRight = player.position.x > transform.position.x;
        patrol.Flip(playerRight ? 1 : -1);
    }

    // SHOOT
    public void Shoot()
    {
        if (!detect.HasTarget || (hp != null && hp.IsDead)) return;

        Transform player = detect.player;
        if (player == null) return;

        GameObject bullet = GetFreeBullet();
        if (bullet == null) return;

        bullet.transform.position = shootPoint.position;
        bullet.SetActive(true);

        Vector2 dir = (player.position - shootPoint.position).normalized;

        EnemyProjectile p = bullet.GetComponent<EnemyProjectile>();
        if (p != null)
            p.SetDirection(dir);
    }

    private GameObject GetFreeBullet()
    {
        foreach (GameObject b in bullets)
            if (!b.activeInHierarchy)
                return b;

        return null;
    }
}
