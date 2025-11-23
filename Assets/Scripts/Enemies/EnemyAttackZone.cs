using UnityEngine;

public class EnemyAttackZone : MonoBehaviour
{
    private EnemyAttack enemyAttack;

    private void Awake()
    {
        enemyAttack = GetComponentInParent<EnemyAttack>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemyAttack.canAttack = true;
            enemyAttack.target = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemyAttack.canAttack = false;
            enemyAttack.target = null;
        }
    }
}
