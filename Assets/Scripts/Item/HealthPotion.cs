using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    public float healAmount = 50f; 
    public bool destroyOnUse = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Entity_Health health = other.GetComponent<Entity_Health>();

        if (health != null && !health.IsDead)
        {
            health.Heal(healAmount);

            if (destroyOnUse)
                Destroy(gameObject);
        }
    }
}
