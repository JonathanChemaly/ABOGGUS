using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WeaponDamageStats
{
    public static int swordDamage = 20, defaultSwordDamage = 20;
    public static int windDamage = 5, defaultWindDamage = 5;
    public static int windAOEDamage = 3, defaultWindAOEDamage = 5;

    public static void resetDamageStats()
    {
        swordDamage = defaultSwordDamage;
        windDamage = defaultWindDamage;
        windAOEDamage = defaultWindAOEDamage;
    }
}
