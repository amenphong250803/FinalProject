using UnityEngine;

public class SavedPickup : MonoBehaviour
{
    [Header("ID")]
    [SerializeField] private string pickupId;

    private string Key => "Pickup_" + pickupId;

    private void Start()
    {
        if (string.IsNullOrEmpty(pickupId))
        {
            return;
        }

        if (PlayerPrefs.GetInt(Key, 0) == 1)
        {
            gameObject.SetActive(false);
        }
    }

    public void MarkCollected()
    {
        if (string.IsNullOrEmpty(pickupId)) return;

        PlayerPrefs.SetInt(Key, 1);
        PlayerPrefs.Save();
    }
}
