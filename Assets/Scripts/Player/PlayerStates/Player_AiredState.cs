using UnityEngine;

public class Player_AiredState : PlayerState
{
    public Player_AiredState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {

    }

    public override void Update()
    {
        base.Update();

        if (input.Player.Jump.WasPerformedThisFrame() && player.jumpCount < player.maxJumps)
        {
            stateMachine.ChangeState(player.jumpState);
            return;
        }

        if (player.moveInput.x != 0)
        {
            player.SetVelocity(player.moveInput.x * player.moveSpeed, rb.linearVelocity.y);
        }
    }
}
