using Unity.VisualScripting;
using UnityEngine;

public class PlayerDashState : EntityState
{
    private float originalGravityScale;
    private int dashDir;

    public PlayerDashState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        dashDir = player.facingDir;

        stateTimer = player.dashDuration;

        originalGravityScale = rb.gravityScale;

        rb.gravityScale = 0;
    }

    public override void Exit()
    {
        base.Exit();

        player.SetVelocity(0, 0);

        rb.gravityScale = originalGravityScale;
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(player.dashSpeed * dashDir, 0);

        CancelDashIfNeeded();

        if (stateTimer < 0f)
        {
            if (player.groundDetected)
                stateMachine.ChangeState(player.idleState);
            else
                stateMachine.ChangeState(player.fallState);
        }
    }

    private void CancelDashIfNeeded()
    {
        if (player.wallDetected && !player.groundDetected)
        {
            stateMachine.ChangeState(player.wallSlideState);
        }
    }
}
