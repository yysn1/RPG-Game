using UnityEngine;

public class PlayerDeadState : PlayerState
{
    public PlayerDeadState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        rb.linearVelocity = Vector2.zero;
        rb.simulated = false;

        input.Disable();
    }
}
