using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    private float controlLockTime = 0.2f;

    public PlayerWallJumpState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        controlLockTime = .2f;

        player.SetVelocity(player.wallJumpForce.x * -player.facingDir, player.wallJumpForce.y);
    }

    public override void Update()
    {
        base.Update();

        controlLockTime -= Time.deltaTime;

        if (controlLockTime < 0)
        {
            if (player.moveInput.x != 0)
            {
                player.SetVelocity(player.moveInput.x * (player.moveSpeed * player.inAirMoveMultiplier), rb.linearVelocity.y);
            }
        }

        if (rb.linearVelocity.y < 0f)
        {
            player.SetVelocity(0, rb.linearVelocity.y);
            stateMachine.ChangeState(player.fallState);
        }

        if (player.wallDetected)
        {
            stateMachine.ChangeState(player.wallSlideState);
        }
    }
}
