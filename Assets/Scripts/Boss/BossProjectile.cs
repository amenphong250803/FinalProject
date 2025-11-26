using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float speed = 6f;
    public float lifetime = 4f;
    public float damage = 15f;
    public LayerMask targetMask;

    private Vector2 direction;
    private float timer;

    void OnEnable()
    {
        timer = 0f;
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        timer += Time.deltaTime;
        if (timer > lifetime)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & targetMask) != 0)
        {
            Entity_Health hp = collision.GetComponent<Entity_Health>();
            if (hp != null)
                hp.TakeDamage(damage);

            gameObject.SetActive(false);
        }
    }
}
