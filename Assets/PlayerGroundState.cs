using UnityEngine;

public class PlayerGroundState : EntityState
{
    public PlayerGroundState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (rb.linearVelocity.y < 0f)
        {
            stateMachine.ChangeState(player.fallState);
        }

        if (input.Player.Jump.WasPerformedThisFrame())
        {
            stateMachine.ChangeState(player.jumpState);
        }
    }
}
