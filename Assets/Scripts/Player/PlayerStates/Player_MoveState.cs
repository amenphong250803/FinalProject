using UnityEngine;

public class Player_MoveState : Player_GroundedState
{
    private float footstepTimer = 0f;
    private float footstepInterval = 0.4f;   // Thời gian giữa các tiếng bước chân

    public Player_MoveState(Player player, StateMachine stateMachine, string stateName)
        : base(player, stateMachine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        footstepTimer = 0f;   // reset khi vừa vào state
    }

    public override void Update()
    {
        base.Update();

        // Nếu không có input → trở về idle
        if (player.moveInput.x == 0)
        {
            stateMachine.ChangeState(player.idleState);
            return;
        }

        // ⭐ Cài tốc độ di chuyển
        player.SetVelocity(player.moveInput.x * player.moveSpeed, player.rb.linearVelocity.y);

        // ⭐ ÂM BƯỚC CHÂN
        footstepTimer -= Time.deltaTime;

        if (footstepTimer <= 0f)
        {
            player.PlayFootstepSFX();      // 🎧 GỌI ÂM BƯỚC CHÂN

            // khoảng cách thời gian giữa các bước chân
            footstepTimer = footstepInterval;
        }
    }
}
