using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABOGGUS.PlayerObjects;

public static class UpgradeStats
{
    public static int swordDamInc = 5;
    public static float healthInc = 5f;
    public static float healthBarInc = 0.025f;
    public static int healthUpgradeCount = 0;
    public static int swordUpgradeCount = 0;
    public static float healthBarSize = 1f, defaultHealthBarSize = 1f;
    public static int mana = 100, defaultMana = 100, totalMana = 100;

    public static int healthUpCost = -10;
    public static int swordUpCost = -10;

    public static int runs = 0;

    public static void IncHealth()
    {
        GameObject.Find("PlayerScripts").GetComponent<Player>().inventory.maxHealth += healthInc;
        GameObject.Find("PlayerScripts").GetComponent<Player>().inventory.health += healthInc;
        var tempScale = GameObject.Find("HealthBar").GetComponent<RectTransform>().localScale;
        tempScale.x += healthBarInc;
        GameObject.Find("HealthBar").GetComponent<RectTransform>().localScale = tempScale;
        Debug.Log(GameObject.Find("PlayerScripts").GetComponent<Player>().inventory.health);
        healthUpgradeCount += 1;
        healthBarSize += healthBarInc;
        GameObject.Find("PlayerScripts").GetComponent<Player>().updateMana(healthUpCost);
    }

    public static void IncDamageSword()
    {
        GameObject.Find("Sword").GetComponent<SwordAttack>().damage+=swordDamInc;
        WeaponDamageStats.swordDamage += UpgradeStats.swordDamInc;
        Debug.Log(GameObject.Find("Sword").GetComponent<SwordAttack>().damage);
        swordUpgradeCount += 1;
        GameObject.Find("PlayerScripts").GetComponent<Player>().updateMana(swordUpCost);
    }

    public static void resetPlayerStats()
    {
        runs = 0;
        healthUpgradeCount = 0;
        swordUpgradeCount = 0;
        healthBarSize = defaultHealthBarSize;
        mana = defaultMana;
    }
}
