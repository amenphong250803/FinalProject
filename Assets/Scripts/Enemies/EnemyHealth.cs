using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHP = 50;
    public int currentHP;
    public EnemyPatrol patrol;

    private void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        if (currentHP <= 0)
        {
            currentHP = 0;
            Die();
        }
    }

    void Die()
    {
        if (patrol != null)
            patrol.enabled = false; // ngừng di chuyển

        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        Debug.Log("☠️ Enemy dead!");
    }
}
