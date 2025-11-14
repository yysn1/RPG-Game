using System;
using UnityEngine;

[Serializable]
public class StatDefenseGroup
{
    //  Physical defense
    public Stat armor;
    public Stat evasion;

    //  Elemental resistance
    public Stat fireRes;
    public Stat iceRes;
    public Stat lightningRes;
}
