using UnityEngine;

public class Enemy : Entity
{
    public EnemyIdleState idleState;
    public EnemyMoveState moveState;
    public EnemyAttackState attackState;

    [Header("Movement details")]
    public float moveSpeed = 1.4f;
    public float idleTime = 2f;

    [Range(0, 2)]
    public float moveAnimSpeedMultiplier = 1f;
}
