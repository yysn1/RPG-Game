using UnityEngine;

public class EnemyAttackState : EnemyHitState
{
    public EnemyAttackState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (ShouldRetreat() && enemy.PlayerDetection())
        {
            stateMachine.ChangeState(enemy.retreatState);
        }

        if (triggerCalled)
        {
            stateMachine.ChangeState(enemy.battleState);
        }
    }

    private bool ShouldRetreat() => DistanceToPlayer() < enemy.minRetreatDistance;
}
