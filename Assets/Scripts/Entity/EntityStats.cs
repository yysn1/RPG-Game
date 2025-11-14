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
}
