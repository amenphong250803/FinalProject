using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    public int potionAmount = 1;   

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerPotionHandler potionHandler = other.GetComponent<PlayerPotionHandler>();

        if (potionHandler != null)
        {
            potionHandler.AddPotion(potionAmount);
            Destroy(gameObject);
        }
    }
}
