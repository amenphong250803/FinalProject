using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement parameters")]
    [SerializeField] private float speed = 2f;

    [Header("Idle behavior")]
    [SerializeField] private float idleDuration = 2f;
    private float idleTimer;

    private Vector3 initScale;
    private bool movingLeft; 

    private void Awake()
    {
        initScale = enemy.localScale;
    }

    private void Update()
    {
        if (movingLeft)
        {
            // Nếu chưa chạm mép trái → tiếp tục đi trái
            if (enemy.position.x >= leftEdge.position.x)
                MoveInDirection(-1);
            else
                ChangeDirection();
        }
        else
        {
            // Nếu chưa chạm mép phải → tiếp tục đi phải
            if (enemy.position.x <= rightEdge.position.x)
                MoveInDirection(1);
            else
                ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
        // Đứng lại 1 chút rồi đổi hướng
        idleTimer += Time.deltaTime;
        if (idleTimer > idleDuration)
        {
            movingLeft = !movingLeft;
            idleTimer = 0;
        }
    }

    private void MoveInDirection(int direction)
    {
        idleTimer = 0;

        // Quay mặt theo hướng di chuyển
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * direction, initScale.y, initScale.z);

        // Di chuyển enemy
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direction * speed, enemy.position.y, enemy.position.z);
    }

    // Hiển thị vùng patrol trong Scene
    private void OnDrawGizmos()
    {
        if (leftEdge != null && rightEdge != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(leftEdge.position, rightEdge.position);
        }
    }
}
