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
        Text healthCount = healthUpgradeAmount.GetComponent<Text>();
        UpgradeStats.IncHealth();
        int newVal = Int32.Parse(healthCount.text.Substring(1)) + 1;
        healthCount.text = "+" + newVal;
        UpgradeStats.healthUpgradeCount += 1;
        UpgradeStats.healthBarSize += 0.025f;
    }

    public void OnClickSword()
    {
        Text swordCount = swordUpgradeAmount.GetComponent<Text>();
        UpgradeStats.IncDamageSword();
        int newVal = Int32.Parse(swordCount.text.Substring(1)) + 1;
        swordCount.text = "+" + newVal;
        UpgradeStats.swordUpgradeCount += 1;
    }
}
