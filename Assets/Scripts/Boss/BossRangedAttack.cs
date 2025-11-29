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
    public AudioSource audioSource;
    public AudioClip shootSFX;
    void Awake()
    {
        detect = GetComponentInParent<BossTargetDetection>();
        zone = GetComponentInParent<BossAttackZone>();
    }
    public void Shoot()
    {
        if (Time.time < nextShootTime) return;

        if (!detect.HasTarget) return;

        if (!zone.playerInRangedZone) return;

        Transform player = detect.player;
        if (player == null) return;

        GameObject bullet = GetFreeBullet();
        if (bullet == null) return;

        bullet.transform.position = shootPoint.position;

        bullet.SetActive(true);

        Vector2 dir = (player.position - shootPoint.position).normalized;
        bullet.GetComponent<BossProjectile>().SetDirection(dir);

        PlayShootSFX();

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

    private void PlayShootSFX()
    {
        if (audioSource == null || shootSFX == null)
            return;

        audioSource.PlayOneShot(shootSFX);
    }
}
