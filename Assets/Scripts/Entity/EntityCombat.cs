using UnityEngine;

public class EntityCombat : MonoBehaviour
{
    private EntityVFX vfx;
    public float damage = 10f; 

    [Header("Target detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius = 1f;
    [SerializeField] private LayerMask whatIsTarget;

    private void Awake()
    {
        vfx = GetComponent<EntityVFX>();
    }

    public void PerformAttack()
    {
        GetDetectedCollider();

        foreach (var target in GetDetectedCollider())
        {
            IDamgable damgable = target.GetComponent<IDamgable>();
            
            if (damgable == null)
            {
                continue;
            }

            bool targetGotHit = damgable.TakeDamage(damage, transform);
            
            if (targetGotHit)
                vfx.CreateOnHitVFX(target.transform);
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
