public class EnemyAnimationTriggers : EntityAnimationTriggers
{
    private Enemy enemy;
    private EnemyVFX enemyVFX;

    protected override void Awake()
    {
        base.Awake();

        enemy = GetComponentInParent<Enemy>();
        enemyVFX = GetComponentInParent<EnemyVFX>();
    }

    private void EnableCounterWindow()
    {
        enemyVFX.EnableCounterAlert(true);
        enemy.EnableCounterWindow(true);
    }

    private void DisableCounterWindow()
    {
        enemyVFX.EnableCounterAlert(false);
        enemy.EnableCounterWindow(false);
    }
}
