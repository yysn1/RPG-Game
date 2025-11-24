using UnityEngine;

public class EntityCombat : MonoBehaviour
{
    private EntityVFX vfx;
    private EntityStats stats;

    [Header("Target detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius = 1f;
    [SerializeField] private LayerMask whatIsTarget;

    [Header("Status effect details")]
    [SerializeField] private float defaultDuration = 3f;
    [SerializeField] private float chillSlowMultiplier = .2f;

    private void Awake()
    {
        vfx = GetComponent<EntityVFX>();
        stats = GetComponent<EntityStats>();
    }

    public void PerformAttack()
    {
        GetDetectedCollider();

        foreach (var target in GetDetectedCollider())
        {
            IDamgable damagable = target.GetComponent<IDamgable>();
            
            if (damagable == null)
            {
                continue;
            }

            float damage = stats.GetPhysicalDamage(out bool isCrit);
            float elementalDamage = stats.GetElementalDamage(out ElementType element);
            bool targetGotHit = damagable.TakeDamage(damage, elementalDamage, element, transform);
            
            if (element != ElementType.None)
            {
                ApplyStatusEffect(target.transform, element);
            }

            if (targetGotHit)
            {
                vfx.UpdateOnHitColor(element);
                vfx.CreateOnHitVFX(target.transform, isCrit);
            }
        }
    }

    public void ApplyStatusEffect(Transform target, ElementType element)
    {
        EntityStatusHandle statusHandle = target.GetComponent<EntityStatusHandle>();

        if (statusHandle == null)
            return;

        if (element == ElementType.Ice && statusHandle.CanBeApplied(ElementType.Ice))
        {
            statusHandle.ApplyChilledEffect(defaultDuration, chillSlowMultiplier);
        }
    }

    protected Collider2D[] GetDetectedCollider()
    {
        return Physics2D.OverlapCircleAll(targetCheck.position, targetCheckRadius, whatIsTarget);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);
    }
}
