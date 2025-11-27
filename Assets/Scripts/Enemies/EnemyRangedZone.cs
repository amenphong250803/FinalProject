using UnityEngine;

public class EnemyRangedZone : MonoBehaviour
{
    public bool playerInRangedZone;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRangedZone = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRangedZone = false;
    }
}
