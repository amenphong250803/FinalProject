using UnityEngine;

public class BossLaserDamage : MonoBehaviour
{
    public Transform laserAreaCenter;
    public Vector2 laserAreaSize;
    public LayerMask targetMask;
    public float damage = 10f;
    public float damageInterval = 0.2f;

    private Animator anim;
    private float nextDamageTime = 0f;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);

        if (!info.IsName("Laser_Beam"))
            return;

        if (Time.time < nextDamageTime)
            return;

        DealDamage();
        nextDamageTime = Time.time + damageInterval;
    }

    void DealDamage()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(
            laserAreaCenter.position,
            laserAreaSize,
            0f,
            targetMask
        );

        foreach (var h in hits)
        {
            if (h.TryGetComponent(out Entity_Health hp))
            {
                hp.TakeDamage(damage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(laserAreaCenter.position, laserAreaSize);
    }
}
