using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPressUpgrade : MonoBehaviour
{
    [SerializeField] GameObject healthUpgradeAmount;
    [SerializeField] GameObject swordUpgradeAmount;
    private void Awake()
    {
        healthUpgradeAmount.GetComponent<Text>().text = "+" + UpgradeStats.healthUpgradeCount;
        swordUpgradeAmount.GetComponent<Text>().text = "+" + UpgradeStats.swordUpgradeCount;
    }
    public void OnClickHealth()
    {
        if (UpgradeStats.mana >= Math.Abs(UpgradeStats.healthUpCost))
        {
            Text healthCount = healthUpgradeAmount.GetComponent<Text>();
            UpgradeStats.IncHealth();
            int newVal = Int32.Parse(healthCount.text.Substring(1)) + 1;
            healthCount.text = "+" + newVal;
        }
    }

    public void OnClickSword()
    {
        if (UpgradeStats.mana >= Math.Abs(UpgradeStats.swordUpCost))
        {
            Text swordCount = swordUpgradeAmount.GetComponent<Text>();
            UpgradeStats.IncDamageSword();
            int newVal = Int32.Parse(swordCount.text.Substring(1)) + 1;
            swordCount.text = "+" + newVal;
        }
    }
}
