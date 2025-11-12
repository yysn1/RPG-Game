using UnityEngine;

public class EnemyStunnedState : EnemyState
{
    private EnemyVFX vfx;
    public EnemyStunnedState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        vfx = enemy.GetComponent<EnemyVFX>();
    }

    public override void Enter()
    {
        base.Enter();

        vfx.EnableCounterAlertWindow(false);
        enemy.EnableCounterWindow(false);

        stateTimer = enemy.stunnedDuration;
        rb.linearVelocity = new Vector2(enemy.stunnedVelocity.x * -enemy.facingDir, enemy.stunnedVelocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer <= 0f)
        {
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}
