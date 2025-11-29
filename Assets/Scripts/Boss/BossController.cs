using UnityEngine;

public class BossController : MonoBehaviour
{
    private Animator anim;
    private BossHealth hp;
    private BossAttackZone zone;

    private float attackTimer = 4f;
    private float rangedTimer = 8f;
    private float lazerTimer = 15f;

    [Header("Immune Settings")]
    public float immuneDuration = 3f;
    private float immuneTimeLeft;
    private bool immuneCounting = false;

    private bool immuneUsed = false;
    private bool lazerUnlocked = false;
    private bool healUsed = false;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        hp = GetComponent<BossHealth>();
        zone = GetComponent<BossAttackZone>();
    }

    void Update()
    {
        if (hp.IsDead) return;

        float hpPercent = hp.GetHpPercent();

        UpdateImmuneTimer();
        UpdatePhases(hpPercent);
        UpdateAttacks();
    }

    void UpdateImmuneTimer()
    {
        if (!immuneCounting) return;

        immuneTimeLeft -= Time.deltaTime;

        if (immuneTimeLeft <= 0)
        {
            immuneCounting = false;
            hp.SetImmune(false);
            hp.damageMultiplier = 1.3f;
            lazerUnlocked = true;
        }
    }

    void TriggerImmune()
    {
        anim.SetTrigger("immune");
        hp.SetImmune(true);

        immuneTimeLeft = immuneDuration;
        immuneCounting = true;
    }

    void UpdatePhases(float hpPercent)
    {
        if (hpPercent <= 0.5f && !immuneUsed)
        {
            immuneUsed = true;
            TriggerImmune();
        }

        if (hpPercent <= 0.1f && !healUsed)
        {
            healUsed = true;
            anim.SetTrigger("heal");
            hp.HealPercent(0.3f);
        }
    }

    void UpdateAttacks()
    {

        attackTimer -= Time.deltaTime;
        rangedTimer -= Time.deltaTime;
        lazerTimer -= Time.deltaTime;

        if (zone.playerTooClose)
        {
            if (attackTimer <= 0f)
            {
                anim.SetTrigger("attack");
                attackTimer = 4f;
            }
            return;
        }

        if (zone.playerInRangedZone)
        {
            if (rangedTimer <= 0f)
            {
                anim.SetTrigger("ranged");
                rangedTimer = 8f;
            }
            return;
        }

        if (lazerUnlocked && lazerTimer <= 0f)
        {
            anim.SetTrigger("lazer");
            lazerTimer = 15f;
            return;
        }

    }
}
