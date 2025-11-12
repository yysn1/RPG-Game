using UnityEngine;

public class EntityAnimationTriggers : MonoBehaviour
{
    private Entity entity;
    private EntityCombat entityCombat;

    protected virtual void Awake()
    {
        entity = GetComponentInParent<Entity>();
        entityCombat = GetComponentInParent<EntityCombat>();
    }

    private void CurrentStateTrigger()
    {
        entity.CurrentStateAnimationTrigger();
    }

    private void AttackTrigger()
    {
        entityCombat.PerformAttack();
    }
}
