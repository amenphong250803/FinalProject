using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    public int damage = 20;  

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            EnemyHealth hp = col.GetComponent<EnemyHealth>();
            if (hp != null)
            {
                hp.TakeDamage(damage);
            }
        }
    }
}
