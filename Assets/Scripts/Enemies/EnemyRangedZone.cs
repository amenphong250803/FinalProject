using UnityEngine;

public class EnemyRangedZone : MonoBehaviour
{
    private EnemyAttackRanged ranged;

    private void Awake()
    {
        ranged = GetComponentInParent<EnemyAttackRanged>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ranged.SetPlayerInRange(true, other.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ranged.SetPlayerInRange(false, null);
        }
    }
}
