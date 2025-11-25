using UnityEngine;

public class Entity : MonoBehaviour
{
    public Animator anim { get; private set; }

    public Rigidbody2D rb { get; private set; }

    protected StateMachine stateMachine;

    private bool facingRight = true;
    public int facingDir { get; private set; } = 1;

    [Header("Collision detection")]
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] private float groundCheckDistance;
    public bool groundDetected { get; private set; }

    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        stateMachine = new StateMachine();

    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        HandleCollisionDetection();
        stateMachine.UpdateActiveState();
    }

    public void CurrentStateAnimationTrigger()
    {
        stateMachine.currentState.AnimationTrigger();
    }

    public virtual void EntityDeath()
    {

    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        HandleFlip(xVelocity);
    }

    public void HandleFlip(float xVelocity)
    {
        if (xVelocity > 0 && facingRight == false)
        {
            Flip();
        }
        else if (xVelocity < 0 && facingRight)
        {
            Flip();
        }
    }
    private void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
        facingDir = facingDir * -1;
    }

    private void HandleCollisionDetection()
    {
        groundDetected = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance));
    }
}
