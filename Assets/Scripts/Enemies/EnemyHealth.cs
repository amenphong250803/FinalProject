using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP;
    public bool IsDead { get; private set; }

    Animator anim;
    EnemyPatrol patrol;
    Rigidbody2D rb;

    void Awake()
    {
        anim = GetComponent<Animator>();
        patrol = GetComponent<EnemyPatrol>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int dmg)
    {
        if (IsDead) return;

        currentHP -= dmg;
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

    void Die()
    {
        if (IsDead) return;
        IsDead = true;

        // Stop patrol
        if (patrol != null)
        {
            patrol.StopMoving();
            patrol.enabled = false;
        }

        // Stop physics
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static;

        // Play animation once
        anim.ResetTrigger("attack");
        anim.ResetTrigger("hurt");
        anim.SetTrigger("dead");

        Debug.Log("Enemy Died!");
    }
}
