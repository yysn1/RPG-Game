using UnityEngine;

public interface IDamgable
{
    public bool TakeDamage(float damage, float elementalDamage, ElementType element, Transform damageDealer);
}
