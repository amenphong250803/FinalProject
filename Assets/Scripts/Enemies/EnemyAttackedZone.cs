using UnityEngine;

public class EnemyAttackedZone : MonoBehaviour
{
    [Header("State")]
    public bool playerInAttackZone;   // EnemyAttack sẽ đọc biến này

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInAttackZone = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInAttackZone = false;
    }
}
