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
    public AudioClip hitSFX;                   // ⭐ tiếng đòn đánh
    public AudioSource audioSource;            // ⭐ nơi phát âm thanh

    public void Awake()
    {
        stats = GetComponent<Entity_Stats>();

        // ⭐ Nếu bạn chưa kéo AudioSource vào thì tự động lấy trong object
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    public void PerformAttack()
    {
        float attackDamage = stats != null ? stats.GetDamage() : 10f;

        GetDetectedColliders();

        bool hitSomething = false; // kiểm tra có đánh trúng không

        foreach (var target in targetColliders)
        {
            Entity_Health targetHealth = target.GetComponent<Entity_Health>();
            if (targetHealth != null)
            {
                // Gây damage
                targetHealth.TakeDamage(attackDamage);

                hitSomething = true;
            }
        }

        // ⭐ PHÁT TIẾNG ĐÁNH TRÚNG
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
