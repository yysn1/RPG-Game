using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    private EntityVFX entityVFX;
    private Entity entity;

    [SerializeField] protected float currentHp;
    [SerializeField] protected float MaxHp = 100;
    [SerializeField] protected bool isDead = false;

    [Header("On Damage KonckBack")]
    [SerializeField] protected Vector2 knockbackPower = new Vector2(1.5f, 2.5f);
    [SerializeField] protected Vector2 heavyKnockbackPower = new Vector2(7f, 7f);
    [SerializeField] protected float knockbackDuration = 0.2f;
    [SerializeField] protected float heavyKnockbackDuration = 0.5f;

    [Header("On Heavy Damage")]
    [SerializeField] protected float heavyDamageThreshold = .3f; // % of max HP

    protected virtual void Awake()
    {
        entityVFX = GetComponent<EntityVFX>();
        entity = GetComponent<Entity>();

        currentHp = MaxHp;
    }

    public virtual void TakeDamage(float damage, Transform damageDealer)
    {
        if (isDead)
            return;

        Vector2 knockback = CalculateKnockback(damage, damageDealer);
        float duration = CalculateDuration(damage);

        entity?.ReciveKnockback(knockback, duration);
        entityVFX?.PlayOnDamageVFX();
        ReduceHp(damage);
    }

    protected void ReduceHp(float damage)
    {
        currentHp -= damage;

        if (currentHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        Debug.Log($"{gameObject.name} has died.");
    }

    private Vector2 CalculateKnockback(float damage, Transform damageDealer)
    {
        int direction = transform.position.x > damageDealer.position.x ? 1 : -1;
        Vector2 knockback = IsHeavyDamage(damage) ? heavyKnockbackPower : knockbackPower;
        knockback.x *= direction;
        
        return knockback;
    }

    private float CalculateDuration(float damage) => IsHeavyDamage(damage) ? heavyKnockbackDuration : knockbackDuration;

    private bool IsHeavyDamage(float damage) => damage / MaxHp >= heavyDamageThreshold;
}
