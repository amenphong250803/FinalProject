using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHP = 50;
    public int currentHP;

    public EnemyPatrol patrolScript;   // ?? t?t khi h?t HP

    void Start()
    {
        currentHP = maxHP;
        patrolScript = GetComponent<EnemyPatrol>(); // t? l?y script patrol
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Qu�i ??ng im
        if (patrolScript != null)
            patrolScript.enabled = false;

        // T?t velocity ?? n� ??ng im ho�n to�n
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = Vector2.zero;

        Debug.Log("Enemy died (stand still)");
    }
}
