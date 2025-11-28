using UnityEngine;

public class EnemyHealth : Entity_Health
{
    Animator anim;
    EnemyPatrol patrol;
    Rigidbody2D rb;
    EnemyCombat combat;   // ⭐ thêm combat

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponentInChildren<Animator>();

        patrol = GetComponent<EnemyPatrol>();
        combat = GetComponent<EnemyCombat>();  // ⭐ lấy combat

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

        base.Die(); // isDead = true

        // ⭐ TẮT PATROL
        if (patrol != null)
        {
            patrol.StopMoving();
            patrol.enabled = false;
        }

        // ⭐ TẮT COMBAT
        if (combat != null)
            combat.enabled = false;

        // ⭐ DỪNG CHUYỂN ĐỘNG
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
        }

        // ⭐ RESET ANIM
        anim.ResetTrigger("attack");
        anim.ResetTrigger("hurt");
        anim.SetTrigger("dead");

        Debug.Log("Enemy Died");
    }
}
