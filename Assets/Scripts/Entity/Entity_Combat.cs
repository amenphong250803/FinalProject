using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    public Collider2D[] targetColliders;
    private Entity_Stats stats;

    [Header("Target detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius = 1;
    [SerializeField] private LayerMask whatIsTarget;

    [Header("Attack Sound")]
    public AudioClip hitSFX;
    public AudioSource audioSource;

    public void Awake()
    {
        stats = GetComponent<Entity_Stats>();

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    public void PerformAttack()
    {
        float attackDamage = stats != null ? stats.GetDamage() : 10f;

        GetDetectedColliders();

        bool hitSomething = false;

        foreach (var target in targetColliders)
        {
            Entity_Health targetHealth = target.GetComponent<Entity_Health>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(attackDamage);

                hitSomething = true;
            }
        }

        if (hitSomething && hitSFX != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSFX);
        }
    }

    private void GetDetectedColliders()
    {
        targetColliders = Physics2D.OverlapCircleAll(targetCheck.position, targetCheckRadius, whatIsTarget);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);
    }
}
