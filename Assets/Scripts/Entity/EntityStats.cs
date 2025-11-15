using UnityEngine;
using UnityEngine.Rendering;

public class EntityStats : MonoBehaviour
{
    public Stat maxHealth;
    public StatMajorGroup major;
    public StatOffenseGroup offense;
    public StatDefenseGroup defense;

    public float GetMaxHealth()
    {
        float baseHp = maxHealth.GetValue();
        float bonusHp = major.vitality.GetValue() * 5f;

        return baseHp + bonusHp;
    }

    public float GetEvasion()
    {
        float baseEvasion = defense.evasion.GetValue();
        float bonusEvasion = major.agility.GetValue() * .5f;

        float toltalEvasion = baseEvasion + bonusEvasion;
        float evasionCap = 85f;

        float finalEvasion = Mathf.Clamp(toltalEvasion, 0, evasionCap);

        return finalEvasion;
    }
}
