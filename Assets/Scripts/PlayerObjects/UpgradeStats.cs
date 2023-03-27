using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABOGGUS.PlayerObjects;

public static class UpgradeStats
{
    public static int healthUpgradeCount = 0;

    // Update is called once per frame
    public static void IncHealth()
    {
            GameObject.Find("PlayerScripts").GetComponent<Player>().inventory.maxHealth += 100;
            GameObject.Find("PlayerScripts").GetComponent<Player>().inventory.health += 100;
            //var tempScale = GameObject.Find("HealthBar").GetComponent<RectTransform>().localScale;
           // tempScale.x += 0.1f;
            //GameObject.Find("HealthBar").GetComponent<RectTransform>().localScale = tempScale;
            Debug.Log(GameObject.Find("PlayerScripts").GetComponent<Player>().inventory.health);
    }

    public static void IncDamage(string weaponType)
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            var weapon = GameObject.Find(weaponType);
            if (weaponType == "Sword")
            {
                weapon.GetComponent<SwordAttack>().damage+=100;
                Debug.Log(weapon.GetComponent<SwordAttack>().damage);
            }
            else if (weaponType == "Wind")
            {
                weapon.GetComponent<WindAttack>().damage += 100;
                Debug.Log(weapon.GetComponent<WindAttack>().damage);
                weapon.GetComponent<WindAOEAttack>().damage += 100;
                Debug.Log(weapon.GetComponent<WindAOEAttack>().damage);
            }
        }
    }
}
