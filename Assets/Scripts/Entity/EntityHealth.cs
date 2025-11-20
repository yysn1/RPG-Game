using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EntityHealth : MonoBehaviour, IDamgable
{
    private Slider healthBar;
    private EntityVFX entityVFX;
    private Entity entity;
    private EntityStats stats;

    [SerializeField] protected float currentHp;
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
        healthBar = GetComponentInChildren<Slider>();
        stats = GetComponent<EntityStats>();

        currentHp = stats.GetMaxHealth();
        UpdateHealthBar();
    }

    public virtual bool TakeDamage(float damage, float elementalDamage, ElementType element, Transform damageDealer)
    {
        if (isDead)
        {
            return false;
        }

        if (AttackEvaded())
        {
            Debug.Log($"{gameObject.name} evaded the attack");
            return false;
        }

        EntityStats attackerStats = damageDealer.GetComponent<EntityStats>();
        float armorReduction = attackerStats != null ? attackerStats.GetArmorReduction() : 0;

        float resistance = stats.GetElementalResistance(element);
        float finalElementalDamage = elementalDamage * (1 - resistance);

        float mitigation = stats.GetArmorMitigation(armorReduction);
        float finalPhysicalDamage = damage * (1 - mitigation);

        TakeKnockback(damageDealer, finalPhysicalDamage);
        ReduceHp(finalPhysicalDamage + finalElementalDamage);

        return true;
    }

    private void TakeKnockback(Transform damageDealer, float finalDamage)
    {
        Vector2 knockback = CalculateKnockback(finalDamage, damageDealer);
        float duration = CalculateDuration(finalDamage);

        entity?.ReciveKnockback(knockback, duration);
    }

    protected void ReduceHp(float damage)
    {
        entityVFX?.PlayOnDamageVFX();
        currentHp -= damage;
        UpdateHealthBar();

        if (currentHp <= 0)
        {
            Die();
        }
    }

    private bool AttackEvaded() => Random.Range(0, 100) < stats.GetEvasion();

    private void Die()
    {
        isDead = true;
        entity.EntityDead();
    }

    private Vector2 CalculateKnockback(float damage, Transform damageDealer)
    {
        int direction = transform.position.x > damageDealer.position.x ? 1 : -1;
        Vector2 knockback = IsHeavyDamage(damage) ? heavyKnockbackPower : knockbackPower;
        knockback.x *= direction;

        return knockback;
    }

    private void UpdateHealthBar()
    {
        if (healthBar == null)
            return;

        healthBar.value = currentHp / stats.GetMaxHealth();
    }

    private float CalculateDuration(float damage) => IsHeavyDamage(damage) ? heavyKnockbackDuration : knockbackDuration;

    private bool IsHeavyDamage(float damage) => damage / stats.GetMaxHealth() >= heavyDamageThreshold;
}
