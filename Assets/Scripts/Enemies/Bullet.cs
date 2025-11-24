using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 8f;
    public int damage = 10;
    public float lifeTime = 3f;  // sau 3s tự tắt

    private Vector2 direction = Vector2.right;
    private float lifeTimer;

    private void OnEnable()
    {
        // Reset thời gian sống mỗi lần bật lại bullet
        lifeTimer = 0f;
    }

    private void Update()
    {
        // Di chuyển theo hướng đã set
        transform.Translate(direction * speed * Time.deltaTime);

        // Đếm thời gian tồn tại
        lifeTimer += Time.deltaTime;
        if (lifeTimer >= lifeTime)
        {
            gameObject.SetActive(false);  // không Destroy, để tái sử dụng
        }
    }

    public void SetDirection(Vector2 dir)
    {
        if (dir == Vector2.zero)
            dir = Vector2.right;

        direction = dir.normalized;

        // Option: flip sprite theo hướng X (nếu bạn muốn)
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * Mathf.Sign(direction.x);
        transform.localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Gây damage lên player
        if (other.CompareTag("Player"))
        {
            PlayerHealth hp = other.GetComponent<PlayerHealth>();
            if (hp != null)
            {
                hp.TakeDamage(damage);
            }

            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Ground") || other.CompareTag("Wall"))
        {
            // Đụng tường/ground thì tắt
            gameObject.SetActive(false);
        }
    }
}
