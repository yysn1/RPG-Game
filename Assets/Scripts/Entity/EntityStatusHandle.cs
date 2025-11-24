using System.Collections;
using UnityEngine;

public class EntityStatusHandle : MonoBehaviour
{
    private Entity entity;
    private EntityVFX entityVFX;
    private EntityStats stats;
    private ElementType currentEffect = ElementType.None;

    private void Awake()
    {
        stats = GetComponent<EntityStats>();
        entity = GetComponent<Entity>();
        entityVFX = GetComponent<EntityVFX>();
    }

    public void ApplyChilledEffect(float duration, float slowMultiplier)
    {
        float iceResistance = stats.GetElementalResistance(ElementType.Ice);
        float reduceDuration = duration * (1 -  iceResistance);

        StartCoroutine(ChilledEffectCo(reduceDuration, slowMultiplier));
    }

    private IEnumerator ChilledEffectCo(float duration, float slowMultiplier)
    {
        entity.SlowDownEntity(duration, slowMultiplier);
        currentEffect = ElementType.Ice;
        entityVFX.PlayOnStatusVFX(duration, ElementType.Ice);

        yield return new WaitForSeconds(duration);
        currentEffect = ElementType.None;
    }

    public bool CanBeApplied(ElementType element)
    {
        return currentEffect == ElementType.None;
    }
}
