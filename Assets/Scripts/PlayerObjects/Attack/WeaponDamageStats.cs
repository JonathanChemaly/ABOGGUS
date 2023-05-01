using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WeaponDamageStats
{
    public static int swordDamage = 20, defaultSwordDamage = 20;
    public static int spearDamage = 15, defaultSpearDamage = 15;

    public static int windDamage = 10, defaultWindDamage = 10;
    public static int windAOEDamage = 5, defaultWindAOEDamage = 5;

    public static int fireDamage = 30, defaultFireDamage = 30;
    public static int fireAOEDamage = 10, defaultFireAOEDamage = 10;

    public static int natureDamage = 5, defaultNatureDamage = 5;
    public static int natureAOEHealing = 5, defaultNatureAOEHealing = 5;

    public static int waterDamage = 15, defaultWaterDamage = 15;
    public static int waterAOEDamage = 10, defaultWaterAOEDamage = 10;

    public static int windCost = 4, defaultWindCost = 2;
    public static int windAOECost = 4, defaultWindAOECost = 5;

    public static int fireCost = 4, defaultFireCost = 5;
    public static int fireAOECost = 4, defaultFireAOECost = 10;

    public static int natureCost = 4, defaultNatureCost = 5;
    public static int natureAOECost = 4, defaultNatureAOECost = 10;

    public static int waterCost = 4, defaultWaterCost = 5;
    public static int waterAOECost = 4, defaultWaterAOECost = 5;

    public static void ResetDamageStats()
    {
        swordDamage = defaultSwordDamage;
        spearDamage = defaultSpearDamage;
        windDamage = defaultWindDamage;
        windAOEDamage = defaultWindAOEDamage;
        fireDamage = defaultFireDamage;
        fireAOEDamage = defaultFireAOEDamage;
        natureDamage = defaultNatureDamage;
        natureAOEHealing = defaultNatureAOEHealing;
        waterDamage = defaultWaterDamage;
        waterAOEDamage = defaultWaterAOEDamage;
    }
}
