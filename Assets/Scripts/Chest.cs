using UnityEngine;

public class Chest : MonoBehaviour, IDamgable
{
    private Animator anim => GetComponentInChildren<Animator>();
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();
    private EntityVFX fx => GetComponent<EntityVFX>();

    [Header("Chest Settings")]
    [SerializeField] private Vector2 knockback = new Vector2(0f, 5f);

    public bool TakeDamage(float damage, Transform damageDealer)
    {
        fx.PlayOnDamageVFX();
        anim.SetBool("open", true);
        rb.linearVelocity = knockback;
        rb.angularVelocity = Random.Range(-200f, 200f);

        return true;
    }

}
