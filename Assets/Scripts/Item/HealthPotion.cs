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
            if (FloatingTextManager.Instance != null)
            {
                FloatingTextManager.Instance.ShowText(
                    "+1",                         
                    other.transform.position + Vector3.up * 1.5f,
                    Color.white              
                );
            }

            SavedPickup saved = GetComponent<SavedPickup>();
            if (saved != null)
                saved.MarkCollected();

            Destroy(gameObject);
        }
    }
}
