using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP;

    private Animator anim;
    private EnemyPatrol patrol;
    private Rigidbody2D rb;
    public bool isDead = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        patrol = GetComponent<EnemyPatrol>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHP -= amount;

        if (currentHP <= 0)
        {
            currentHP = 0;
            Die();
        }
        else
        {
            anim.SetTrigger("hurt");
        }
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;

        if (patrol != null)
        {
            patrol.StopMoving();
            patrol.enabled = false;
        }

        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;

        anim.SetTrigger("dead");
    }
}
