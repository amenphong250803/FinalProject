using UnityEngine;

public class EnemyRangedZone : MonoBehaviour
{
    private EnemyAttackRanged enemy;

    private void Awake()
    {
        // Tìm EnemyAttackRanged trên object cha (enemy)
        enemy = GetComponentInParent<EnemyAttackRanged>();
        if (enemy == null)
        {
            Debug.LogError("❌ EnemyRangedZone không tìm thấy EnemyAttackRanged ở parent!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (enemy == null) return;

        if (other.CompareTag("Player"))
        {
            enemy.SetPlayerInRange(true, other.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (enemy == null) return;

        if (other.CompareTag("Player"))
        {
            enemy.SetPlayerInRange(false, null);
        }
    }
}
