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

        if (!zone.playerInRangedZone)
        {
            if (wasInZone)
            {
                StopAttackAndResumePatrol();
                wasInZone = false;
            }
            return;
        }

        wasInZone = true;

        if (patrol != null)
            patrol.StopPatrol();

        FlipTowardPlayer();

        if (!detect.HasTarget) return;
        if (Time.time < nextShootTime) return;

        anim.SetTrigger("attack");
        anim.SetBool("attack", true);

        nextShootTime = Time.time + cooldown;
    }

    private void StopAttackAndResumePatrol()
    {
        anim.ResetTrigger("attack");
        anim.SetBool("attack", false);

        if (patrol != null)
            patrol.ResumePatrol();
    }

    private void FlipTowardPlayer()
    {
        if (!detect.HasTarget || patrol == null) return;

        Transform player = detect.player;
        if (player == null) return;

        bool right = player.position.x > transform.position.x;
        patrol.Flip(right ? 1 : -1);
    }

    public void Shoot()
    {
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
