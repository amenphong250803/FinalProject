using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    public int damage = 50;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
