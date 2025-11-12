using UnityEngine;

public class EntityCombat : MonoBehaviour
{
    public float damage = 10f;  

    [Header("Target detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius = 1f;
    [SerializeField] private LayerMask whatIsTarget;

    public void PerformAttack()
    {
        GetDetectedCollider();

        foreach (var target in GetDetectedCollider())
        {
            IDamgable damgable = target.GetComponent<IDamgable>();
            damgable?.TakeDamage(damage, transform);
        }
    }

    private Collider2D[] GetDetectedCollider()
    {
        return Physics2D.OverlapCircleAll(targetCheck.position, targetCheckRadius, whatIsTarget);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);
    }
}
