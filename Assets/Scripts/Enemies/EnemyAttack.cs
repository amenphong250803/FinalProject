using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damage = 10;
    public float attackRange = 1.5f;
    public float attackCooldown = 1f;

    private float lastAttack = 0f;
    private Transform player;
    private EnemyHealth hp;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        hp = GetComponent<EnemyHealth>();
    }

    void Update()
    {
        if (hp.currentHP <= 0) return;

        float dist = Vector2.Distance(transform.position, player.position);

        if (dist <= attackRange)
        {
            if (Time.time >= lastAttack + attackCooldown)
            {
                lastAttack = Time.time;

                PlayerHealth ph = player.GetComponent<PlayerHealth>();
                if (ph != null)
                {
                    ph.TakeDamage(damage);
                    Debug.Log("Enemy did damage! Player HP = " + ph.currentHP);
                }
                else Debug.Log("PlayerHealth NOT FOUND!");
            }
        }
    }
}
