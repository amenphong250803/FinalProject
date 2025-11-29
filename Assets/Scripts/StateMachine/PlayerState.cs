using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class PlayerState : EntityState
{
    protected Player player;
    protected PlayerInputSet input;

    protected Entity_Health health;

    public PlayerState(Player player, StateMachine stateMachine, string animBoolName) : base(stateMachine, animBoolName)
    {
        this.player = player;

        anim = player.anim;
        rb = player.rb;
        input = player.input;

        health = player.GetComponent<Entity_Health>();
    }

    public override void Update()
    {
        base.Update();

        if (input.Player.Dash.WasPressedThisFrame() && Time.time >= player.lastDashTime + player.dashCooldown && stateMachine.currentState != player.dashState)
        {
            stateMachine.ChangeState(player.dashState);
        }
    }

    public override void UpdateAnimationParameters()
    {
        base.UpdateAnimationParameters();
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
    }
}
