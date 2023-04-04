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

    public static void resetDamageStats()
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
