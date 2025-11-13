using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EntityHealth : MonoBehaviour, IDamgable
{
    private Slider healthBar;
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
        healthBar = GetComponentInChildren<Slider>();

        currentHp = MaxHp;
        UpdateHealthBar();
    }

    public virtual void TakeDamage(float damage, Transform damageDealer)
    {
        if (isDead)
        {
            return;
        }

        Vector2 knockback = CalculateKnockback(damage, damageDealer);
        float duration = CalculateDuration(damage);

        entity?.ReciveKnockback(knockback, duration);
        entityVFX?.PlayOnDamageVFX();
        ReduceHp(damage);
    }

    protected void ReduceHp(float damage)
    {
        currentHp -= damage;
        UpdateHealthBar();

        if (currentHp <= 0)
        {
            Die();
        }
    }

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

        healthBar.value = currentHp / MaxHp;
    }

    private float CalculateDuration(float damage) => IsHeavyDamage(damage) ? heavyKnockbackDuration : knockbackDuration;

    private bool IsHeavyDamage(float damage) => damage / MaxHp >= heavyDamageThreshold;
}
