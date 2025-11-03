using UnityEngine;

public abstract class PlayerState : EntityState
{
    protected Player player;
    protected InputSystem_Actions input;

    public PlayerState(Player player, StateMachine stateMachine, string animBoolName) : base(stateMachine, animBoolName)
    {
        this.player = player;

        anim = player.anim;
        rb = player.rb;
        input = player.input;
    }

    public override void Update()
    {
        base.Update();

        anim.SetFloat("yVelocity", rb.linearVelocity.y);

        if (input.Player.Dash.WasPressedThisFrame() && canDash())
        {
            stateMachine.ChangeState(player.dashState);
        }
    }

    private bool canDash()
    {
        if (player.wallDetected && !player.groundDetected)
        {
            return false;
        }

        if (stateMachine.currentState == player.dashState)
        {
            return false;
        }

        return true;
    }
}
