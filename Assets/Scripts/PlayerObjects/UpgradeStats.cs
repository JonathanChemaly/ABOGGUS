using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABOGGUS.PlayerObjects;

[System.Serializable]
public static class UpgradeStats
{
    public static int swordDamInc = 5;
    public static int spearDamInc = 5;
    public static float healthInc = 5f;
    public static float healFromManaInc = 5f;
    public static float healFromManaVal = 0f;
    public static int spellDamInc = 2;
    public static int overallSpellDamBonus = 0;
    public static float healthBarInc = 0.025f;

    public static int healthUpgradeCount = 0;
    public static int swordUpgradeCount = 0;
    public static int spearUpgradeCount = 0;
    public static int mEUpgradeCount = 0;
    public static int oSDUpgradeCount = 0;
    public static int hFMUpgradeCount = 0;
    public static int bonusDamUpgradeCount = 0;

    public static float healthBarSize = 1f, defaultHealthBarSize = 1f;
    public static int mana = 10000, defaultMana = 10000, totalMana = 10000;
    public static float manaEfficiency = 1.0f;

    public static int healthUpCost = -10;
    public static int swordUpCost = -10;
    public static int spearUpCost = -15;
    public static int mECost = -20;
    public static int oSDCost = -30;
    public static int hFMCost = -50;
    public static int bonusDamCost = -50;

    public static int runs = 0;

    public static bool canHealFromMana = false;
    public static bool canDealBonusDamAtMaxHealth = false;
    public static float bonusDamMultiplier = 1.0f;

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
        WeaponDamageStats.swordDamage += swordDamInc;
        Debug.Log(GameObject.Find("Sword").GetComponent<SwordAttack>().damage);
        swordUpgradeCount += 1;
        GameObject.Find("PlayerScripts").GetComponent<Player>().updateMana(swordUpCost);
    }

    public static void IncDamageSpear()
    {
        GameObject.Find("Spear").GetComponent<SpearAttack>().damage += spearDamInc;
        WeaponDamageStats.spearDamage += spearDamInc;
        Debug.Log(GameObject.Find("Spear").GetComponent<SpearAttack>().damage);
        spearUpgradeCount += 1;
        GameObject.Find("PlayerScripts").GetComponent<Player>().updateMana(spearUpCost);
    }

    public static void IncManaEfficiency()
    {
        manaEfficiency -= 0.1f;
        Debug.Log(manaEfficiency);
        mEUpgradeCount += 1;
        GameObject.Find("PlayerScripts").GetComponent<Player>().updateMana(mECost);
    }

    public static void IncSpellDamage()
    {
        WeaponDamageStats.windDamage += spellDamInc;

        WeaponDamageStats.windAOEDamage += spellDamInc;

        WeaponDamageStats.fireDamage += spellDamInc;

        WeaponDamageStats.fireAOEDamage += spellDamInc;

        WeaponDamageStats.waterDamage += spellDamInc;

        WeaponDamageStats.waterAOEDamage += spellDamInc;

        WeaponDamageStats.natureDamage += spellDamInc;

        Debug.Log("New Wind Damage: " + WeaponDamageStats.windDamage);
        Debug.Log("New Wind AOE Damage: " + WeaponDamageStats.windAOEDamage);
        Debug.Log("New Fire Damage: " + WeaponDamageStats.fireDamage);
        Debug.Log("New Fire AOE Damage: " + WeaponDamageStats.fireAOEDamage);
        Debug.Log("New Water Damage: " + WeaponDamageStats.waterDamage);
        Debug.Log("New Water AOE Damage: " + WeaponDamageStats.waterAOEDamage);
        Debug.Log("New Nature Damage: " + WeaponDamageStats.natureDamage);


        oSDUpgradeCount += 1;
        overallSpellDamBonus += 2;
        GameObject.Find("PlayerScripts").GetComponent<Player>().updateMana(oSDCost);
    }

    public static void HealFromMana()
    {
        canHealFromMana = true;
        healFromManaVal += healFromManaInc;
        Debug.Log("Health gained from mana: " + healFromManaVal);
        hFMUpgradeCount += 1;
        GameObject.Find("PlayerScripts").GetComponent<Player>().updateMana(hFMCost);
    }

    public static void IncBonusDam()
    {
        canDealBonusDamAtMaxHealth = true;
        bonusDamMultiplier += 0.1f;
        bonusDamUpgradeCount += 1;
        GameObject.Find("PlayerScripts").GetComponent<Player>().updateMana(bonusDamCost);
    }

    public static bool CanDealBonusDamAtMaxHealth()
    {
        return UpgradeStats.canDealBonusDamAtMaxHealth &&
                (GameObject.Find("PlayerScripts").GetComponent<Player>().inventory.health ==
                GameObject.Find("PlayerScripts").GetComponent<Player>().inventory.maxHealth);
    }

    public static void resetPlayerStats()
    {
        runs = 0;
        healthUpgradeCount = 0;
        swordUpgradeCount = 0;
        healthBarSize = defaultHealthBarSize;
        mana = defaultMana;
        totalMana = defaultMana;
        manaEfficiency = 1.0f;
        oSDUpgradeCount = 0;
        overallSpellDamBonus = 0;
        hFMUpgradeCount = 0;
        canHealFromMana = false;
        healFromManaVal = 0;
        bonusDamMultiplier = 1.0f;
        bonusDamUpgradeCount = 0;
        canDealBonusDamAtMaxHealth = false;
    }
}
