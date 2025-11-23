using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP;
    public Image hpFill;

    private Animator anim;
    private SimplePlayerController controller;

    void Start()
    {
        currentHP = maxHP;
        anim = GetComponent<Animator>();
        controller = GetComponent<SimplePlayerController>();

        hpFill.fillAmount = 1f;
    }

    public void TakeDamage(int amount)
    {
        if (currentHP <= 0) return; // Đã chết rồi thì khỏi nhận damage

        currentHP -= amount;
        if (currentHP < 0) currentHP = 0;

        hpFill.fillAmount = (float)currentHP / maxHP;

        // 🎯 Gọi animation Hurt
        anim.SetTrigger("hurt");

        if (currentHP == 0)
        {
            Die();
        }
    }

    void Die()
    {
        anim.SetTrigger("die");

        // Tắt điều khiển player
        controller.enabled = false;

        // Tắt Rigidbody movement
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
        rb.isKinematic = true;

        Debug.Log("💀 Player died!");
    }
}
