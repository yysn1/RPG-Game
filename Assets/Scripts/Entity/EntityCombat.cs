using UnityEngine;

public class EntityCombat : MonoBehaviour
{
    private EntityVFX vfx;
    private EntityStats stats;

    [Header("Target detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius = 1f;
    [SerializeField] private LayerMask whatIsTarget;

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
            
            if (targetGotHit)
                vfx.CreateOnHitVFX(target.transform, isCrit);
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
