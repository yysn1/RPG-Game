using UnityEngine;

public class EnemyHealth : EntityHealth
{
    private Enemy enemy => GetComponent<Enemy>();

    public override bool TakeDamage(float damage, Transform damageDealer)
    {

        if (!base.TakeDamage(damage, damageDealer))
        {
            return false;
        }

        if (isDead)
        {
            return false;
        }

        if (damageDealer.GetComponent<Player>() != null)
        {
            enemy.TryEnterBattleState(damageDealer);
        }

        return true;
    }
}
