using UnityEngine;

public class EnemyHealth : Entity_Health
{
    Animator anim;
    EnemyPatrol patrol;
    Rigidbody2D rb;

    public bool IsDead { get; internal set; }

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        patrol = GetComponent<EnemyPatrol>();
        rb = GetComponent<Rigidbody2D>();
    }

    public override void TakeDamage(float dmg)
    {
        if (isDead) return;

        base.TakeDamage(dmg);

        if (!isDead)
        {
            anim.SetTrigger("hurt");
        }
    }

    protected override void Die()
    {
        if (isDead) return;
        base.Die(); // set isDead = true

        // Stop patrol
        if (patrol != null)
        {
            patrol.StopMoving();
            patrol.enabled = false;
        }

        // Stop physics
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
        }

        // Play animation once
        anim.ResetTrigger("attack");
        anim.ResetTrigger("hurt");
        anim.SetTrigger("dead");

        Debug.Log("Enemy Died!");
    }


}
