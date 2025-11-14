using System;
using UnityEngine;

[Serializable]
public class StatOffenseGroup
{
    //  Physical damage
    public Stat damage;
    public Stat critPower;
    public Stat critChance;

    //  Elemental damage
    public Stat fireDamage;
    public Stat iceDamage;
    public Stat lightningDamage;
}
