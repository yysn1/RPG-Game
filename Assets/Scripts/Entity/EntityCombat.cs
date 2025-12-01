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
    [SerializeField] private float electrifyChargeBuildUp = .4f;
    [Space]
    [SerializeField] private float fireScale = .8f;
    [SerializeField] private float lightningScale = 2.5f;

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
            float elementalDamage = stats.GetElementalDamage(out ElementType element, .6f);
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

    public void ApplyStatusEffect(Transform target, ElementType element, float scaleFactor = 1)
    {
        EntityStatusHandle statusHandle = target.GetComponent<EntityStatusHandle>();

        if (statusHandle == null)
            return;

        if (element == ElementType.Ice && statusHandle.CanBeApplied(ElementType.Ice))
        {
            statusHandle.ApplyChillEffect(defaultDuration, chillSlowMultiplier);
        }

        if (element == ElementType.Fire && statusHandle.CanBeApplied(ElementType.Fire))
        {
            scaleFactor = fireScale;
            float fireDamage = stats.offense.fireDamage.GetValue() * scaleFactor;
            statusHandle.ApplyBurnEffect(defaultDuration, fireDamage);
        }

        if (element == ElementType.Lightning && statusHandle.CanBeApplied(ElementType.Lightning))
        {
            scaleFactor = lightningScale;
            float lightDamage = stats.offense.lightningDamage.GetValue() * scaleFactor;
            statusHandle.ApplyElectrify(defaultDuration, lightDamage, electrifyChargeBuildUp);
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
