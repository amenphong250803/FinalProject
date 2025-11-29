using UnityEngine;

public class BossAttackZone : MonoBehaviour
{
    [Header("Donut Attack Zone")]
    public float innerRadius = 3f;
    public float outerRadius = 7f;

    private BossTargetDetection detect;

    [Header("Zone Status (Debug)")]
    public bool playerTooClose = false;
    public bool playerInRangedZone = false;

    void Awake()
    {
        detect = GetComponent<BossTargetDetection>();
    }

    void Update()
    {
        if (!detect.HasTarget)
        {
            playerTooClose = false;
            playerInRangedZone = false;
            return;
        }

        float dist = detect.distanceToPlayer;

        playerTooClose = dist <= innerRadius;

        playerInRangedZone = dist > innerRadius && dist <= outerRadius;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, innerRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, outerRadius);
    }
}
