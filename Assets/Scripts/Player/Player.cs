using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class Player : Entity
{
    public PlayerInputSet input { get; private set; }
    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }

    public Player_JumpState jumpState { get; private set; }
    public Player_FallState fallState { get; private set; }

    public Player_DashState dashState { get; private set; }
    public Player_BasicAttackState basicAttackState { get; private set; }

    public Player_DeadState deadState { get; private set; }

    [Header("Attack details")]
    public Vector2[] attackVelocity;
    public float attackVelocityDuration = .1f;
    public float comboResetTime = 1;
    private Coroutine queuedAttackCo;

    [Header("Movement details")]
    public float moveSpeed;
    public float jumpForce = 5;
    [Space]
    public float dashDuration = .25f;
    public float dashSpeed = 20;

    [Header("Jump Settings")]
    public int maxJumps = 2;
    [HideInInspector] public int jumpCount = 0;

    [Header("Dash Cooldown")]
    public float dashCooldown = 1.5f;
    [HideInInspector] public float lastDashTime = -999f;


    public Vector2 moveInput { get; private set; }


    [Header("Audio Settings")]
    public AudioSource audioSource;   

    public AudioClip jumpSFX;
    public AudioClip dashSFX;
    public AudioClip footstepSFX;
    public AudioClip deathSFX;     

    protected override void Awake()
    {
        base.Awake();

        input = new PlayerInputSet();

        idleState = new Player_IdleState(this, stateMachine, "idle");
        moveState = new Player_MoveState(this, stateMachine, "move");

        jumpState = new Player_JumpState(this, stateMachine, "jumpFall");
        fallState = new Player_FallState(this, stateMachine, "jumpFall");

        basicAttackState = new Player_BasicAttackState(this, stateMachine, "basicAttack");
        deadState = new Player_DeadState(this, stateMachine, "dead");

        dashState = new Player_DashState(this, stateMachine, "dash");

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    public void PlayJumpSFX()
    {
        if (jumpSFX) audioSource.PlayOneShot(jumpSFX);
    }

    public void PlayDashSFX()
    {
        if (dashSFX) audioSource.PlayOneShot(dashSFX);
    }

    public void PlayFootstepSFX()
    {
        if (footstepSFX) audioSource.PlayOneShot(footstepSFX);
    }

    public void PlayDeathSFX()
    {
        if (deathSFX) audioSource.PlayOneShot(deathSFX);
    }

    public override void EntityDeath()
    {
        base.EntityDeath();

        PlayDeathSFX();

        stateMachine.ChangeState(deadState);
    }

    public void EnterAttackStateWithDelay()
    {
        if (queuedAttackCo != null)
            StopCoroutine(queuedAttackCo);

        queuedAttackCo = StartCoroutine(EnterAttackStateWithDelayCo());
    }

    private IEnumerator EnterAttackStateWithDelayCo()
    {
        yield return new WaitForEndOfFrame();
        stateMachine.ChangeState(basicAttackState);
    }

    private void OnEnable()
    {
        input.Enable();

        input.Player.Movement.performed += context => moveInput = context.ReadValue<Vector2>();
        input.Player.Movement.canceled += context => moveInput = Vector2.zero;
    }

    private void OnDisable()
    {
        input.Disable();
    }
}
