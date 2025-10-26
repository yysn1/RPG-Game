using UnityEngine;

public class PlayerMoveState : EntityState
{
    public PlayerMoveState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
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

        if (player.moveInput.x == 0)
        {
            stateMachine.ChangeState(player.idleState);
        }

        player.SetVelocity(player.moveInput.x * player.moveSpeed, rb.linearVelocity.y);
    }
}
