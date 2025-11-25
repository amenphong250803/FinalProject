using UnityEngine;

public class EnemyHealth : Entity_Health
{
    Animator anim;
    EnemyPatrol patrol;
    Rigidbody2D rb;

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponentInChildren<Animator>();

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

        base.Die(); // làm isDead = true

        if (patrol != null)
        {
            patrol.StopMoving();
            patrol.enabled = false;
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
        }

        anim.ResetTrigger("attack");
        anim.ResetTrigger("hurt");
        anim.SetTrigger("dead");

        Debug.Log("Enemy Died");
    }
}
