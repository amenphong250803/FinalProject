using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    public Slider playerHealthSlider;      
    public Entity_Health playerHealth;     

    private void Start()
    {
        if (playerHealth == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
                playerHealth = p.GetComponent<Entity_Health>();
        }
    }

    private void Update()
    {
        if (playerHealth == null || playerHealthSlider == null)
            return;

        float maxHp = playerHealth.GetMaxHealth();
        if (maxHp <= 0) maxHp = 1;

        playerHealthSlider.value = playerHealth.GetCurrentHp() / maxHp;
    }
}
