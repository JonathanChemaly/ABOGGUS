using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPressUpgrade : MonoBehaviour
{
    [SerializeField] GameObject healthUpgradeAmount;
    private void Awake()
    {
        healthUpgradeAmount.GetComponent<Text>().text = "+" + UpgradeStats.healthUpgradeCount;
    }
    public void OnClickHealth()
    {
        Text healthCount = healthUpgradeAmount.GetComponent<Text>();
        Debug.Log(healthCount);
        UpgradeStats.IncHealth();
        int newVal = Int32.Parse(healthCount.text.Substring(1)) + 1;
        healthCount.text = "+" + newVal;
        GameObject.Find("HUD").GetComponent<PlayerHUD>().IncreaseHealthBar();
        UpgradeStats.healthUpgradeCount += 1;
    }
}
