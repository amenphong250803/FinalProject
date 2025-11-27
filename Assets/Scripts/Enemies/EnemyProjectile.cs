using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed = 7f;
    public float lifetime = 4f;
    public float damage = 10f;

    private Vector2 direction;
    private float timer;

    private void OnEnable()
    {
        timer = 0f;
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        timer += Time.deltaTime;
        if (timer >= lifetime)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Entity_Health hp = col.GetComponent<Entity_Health>();
            if (hp != null)
                hp.TakeDamage(damage);

            gameObject.SetActive(false);
        }
    }
}
