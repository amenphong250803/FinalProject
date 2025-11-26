using UnityEngine;

public class BossAttackZone : MonoBehaviour
{
    [Header("Donut Attack Zone")]
    public float innerRadius = 3f;     // Zone gần → chỉ melee
    public float outerRadius = 7f;     // Zone xa → ranged

    private BossTargetDetection detect;

    [Header("Zone Status (Debug)")]
    public bool playerTooClose = false;   // Melee attack zone
    public bool playerInRangedZone = false; // Donut zone

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

        // Vùng Melee (gần)
        playerTooClose = dist <= innerRadius;

        // Vùng Ranged (donut)
        playerInRangedZone = dist > innerRadius && dist <= outerRadius;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;      // Melee zone
        Gizmos.DrawWireSphere(transform.position, innerRadius);

        Gizmos.color = Color.yellow;   // Ranged zone
        Gizmos.DrawWireSphere(transform.position, outerRadius);
    }
}
