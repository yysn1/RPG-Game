using System;
using UnityEngine;

[Serializable]
public class StatOffenseGroup
{
    public Stat attackSpeed;

    //  Physical damage
    public Stat damage;
    public Stat critPower;
    public Stat critChance;
    public Stat armorReduction;

    //  Elemental damage
    public Stat fireDamage;
    public Stat iceDamage;
    public Stat lightningDamage;
}
