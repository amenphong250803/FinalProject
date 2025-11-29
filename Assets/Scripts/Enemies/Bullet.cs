using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 8f;
    public int damage = 10;
    public float lifeTime = 3f;

    private Vector2 direction = Vector2.right;
    private float lifeTimer;

    private void OnEnable()
    {
        lifeTimer = 0f;
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        // Đếm thời gian tồn tại
        lifeTimer += Time.deltaTime;
        if (lifeTimer >= lifeTime)
        {
            gameObject.SetActive(false);
        }
    }

    public void SetDirection(Vector2 dir)
    {
        if (dir == Vector2.zero)
            dir = Vector2.right;

        direction = dir.normalized;

        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * Mathf.Sign(direction.x);
        transform.localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            Entity_Health hp = other.GetComponent<Entity_Health>();
            if (hp != null)
            {
                hp.TakeDamage(damage);
            }

            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Ground"))
        {
            gameObject.SetActive(false);
        }
    }
}
