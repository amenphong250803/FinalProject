using UnityEngine;

public class EnemyHealth : Entity_Health
{
    Animator anim;
    EnemyPatrol patrol;
    Rigidbody2D rb;
    EnemyCombat combat;

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponentInChildren<Animator>();

        patrol = GetComponent<EnemyPatrol>();
        combat = GetComponent<EnemyCombat>();

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

        EnemyKillReward reward = GetComponent<EnemyKillReward>();
        if (reward != null)
        {
            reward.RewardKill();
        }

        PlayerProgression prog = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerProgression>();
        if (prog != null)
        {
            prog.AddKill();
        }

        if (patrol != null)
        {
            patrol.StopMoving();
            patrol.enabled = false;
        }

        if (combat != null)
            combat.enabled = false;

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
        }

        anim.ResetTrigger("attack");
        anim.ResetTrigger("hurt");
        anim.SetTrigger("dead");

    }
}
