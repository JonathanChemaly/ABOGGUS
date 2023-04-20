using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABOGGUS.PlayerObjects;

public class BuffManager : MonoBehaviour
{
    public static BuffManager Instance { get; private set; }

    public static int swordDamInc = 5;
    public static float healthInc = 5f;
    public static float healthBarInc = 0.025f;

    private int healthBuffNum = 0;
    private int attackBuffNum = 0;

    private float defaultHealth = 0;
    private int defaultAttack = 0;
    private float defaultHealthBarSize;
    private Vector3 defaultHealthbarScale;
    // Start is called before the first frame update
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void StoreStatsBeforeRun()
    {
        defaultHealth = GameObject.Find("PlayerScripts").GetComponent<Player>().inventory.maxHealth;
        defaultAttack = GameObject.Find("Sword").GetComponent<SwordAttack>().damage;
        defaultHealthBarSize = UpgradeStats.healthBarSize;
        defaultHealthbarScale = GameObject.Find("HealthBar").GetComponent<RectTransform>().localScale;
    }

    public void RemoveBuffs()
    {
        GameObject.Find("PlayerScripts").GetComponent<Player>().inventory.maxHealth = defaultHealth;
        GameObject.Find("PlayerScripts").GetComponent<Player>().inventory.health = defaultHealth;
        UpgradeStats.healthBarSize = defaultHealthBarSize;
        GameObject.Find("HealthBar").GetComponent<RectTransform>().localScale = defaultHealthbarScale;
        GameObject.Find("Sword").GetComponent<SwordAttack>().damage = defaultAttack;
    }

    public void BuffHealth()
    {
        GameObject.Find("PlayerScripts").GetComponent<Player>().updateMana(-UpgradeStats.healthUpCost);
        UpgradeStats.IncHealth();
        healthBuffNum++;
        UpgradeStats.healthUpgradeCount--;
    }

    public void BuffAttack()
    {
        GameObject.Find("PlayerScripts").GetComponent<Player>().updateMana(-UpgradeStats.swordUpCost);
        UpgradeStats.IncDamageSword();
        attackBuffNum++;
        UpgradeStats.swordUpgradeCount--;
    }
}
