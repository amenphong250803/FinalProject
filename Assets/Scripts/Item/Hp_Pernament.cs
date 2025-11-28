using UnityEngine;

public class Hp_Pernament : MonoBehaviour
{
    public int amount = 1; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerPermanentHpHandler handler = other.GetComponent<PlayerPermanentHpHandler>();

        if (handler != null)
        {
            handler.AddPermanentHpItem(amount);

            if (FloatingTextManager.Instance != null)
            {
                FloatingTextManager.Instance.ShowText(
                    "+1",
                    other.transform.position + Vector3.up * 1.5f,
                    new Color(0.5f, 0.8f, 1f)
                );
            }

            SavedPickup saved = GetComponent<SavedPickup>();
            if (saved != null)
                saved.MarkCollected();

            Destroy(gameObject);
        }
    }
}
