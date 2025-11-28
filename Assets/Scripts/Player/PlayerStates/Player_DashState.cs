using UnityEngine;

public class Player_DashState : PlayerState
{
    private float originalGravityScale;
    private int dashDir;

    public Player_DashState(Player player, StateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // ⭐ PHÁT ÂM DASH ⭐
        player.PlayDashSFX();

        if (health != null)
            health.SetInvulnerable(true);

        // hướng dash
        dashDir = player.moveInput.x != 0 ? ((int)player.moveInput.x) : player.facingDir;

        stateTimer = player.dashDuration;

        // tắt gravity khi dash
        originalGravityScale = rb.gravityScale;
        rb.gravityScale = 0;
    }

    public override void Update()
    {
        base.Update();

        // giữ vận tốc dash
        player.SetVelocity(player.dashSpeed * dashDir, 0);

        // hết thời gian dash → chuyển state
        if (stateTimer < 0)
        {
            if (player.groundDetected)
                stateMachine.ChangeState(player.idleState);
            else
                stateMachine.ChangeState(player.fallState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        if (health != null)
            health.SetInvulnerable(false);

        player.SetVelocity(0, 0);

        // trả lại gravity
        rb.gravityScale = originalGravityScale;
    }
}
