using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public PlayerHealth player;
    public Image fill;

    void Update()
    {
        float hpPercent = (float)player.currentHP / player.maxHP;
        fill.fillAmount = hpPercent;
    }
}
