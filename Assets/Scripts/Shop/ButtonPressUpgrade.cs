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
    [SerializeField] GameObject spearUpgradeAmount;
    [SerializeField] GameObject manaEfficiencyUpgradeAmount;
    [SerializeField] GameObject spellDamageUpgradeAmount;
    [SerializeField] GameObject currentManaHolder;
    private void Awake()
    {
        healthUpgradeAmount.GetComponent<Text>().text = "+" + UpgradeStats.healthUpgradeCount;
        swordUpgradeAmount.GetComponent<Text>().text = "+" + UpgradeStats.swordUpgradeCount;
        spearUpgradeAmount.GetComponent<Text>().text = "+" + UpgradeStats.spearUpgradeCount;
        manaEfficiencyUpgradeAmount.GetComponent<Text>().text = "+" + UpgradeStats.mEUpgradeCount;
        spellDamageUpgradeAmount.GetComponent<Text>().text = "+" + UpgradeStats.oSDUpgradeCount;
    }
    public void OnClickHealth()
    {
        if (UpgradeStats.mana >= Math.Abs(UpgradeStats.healthUpCost))
        {
            Text healthCount = healthUpgradeAmount.GetComponent<Text>();
            UpgradeStats.IncHealth();
            int newVal = Int32.Parse(healthCount.text.Substring(1)) + 1;
            healthCount.text = "+" + newVal;
            currentManaHolder.GetComponent<Text>().text = UpgradeStats.mana.ToString();
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
            currentManaHolder.GetComponent<Text>().text = UpgradeStats.mana.ToString();
        }
    }

    public void OnClickSpear()
    {
        if (UpgradeStats.mana >= Math.Abs(UpgradeStats.spearUpCost))
        {
            Text spearCount = spearUpgradeAmount.GetComponent<Text>();
            UpgradeStats.IncDamageSpear();
            int newVal = Int32.Parse(spearCount.text.Substring(1)) + 1;
            spearCount.text = "+" + newVal;
            currentManaHolder.GetComponent<Text>().text = UpgradeStats.mana.ToString();
        }
    }

    public void OnClickME()
    {
        if (UpgradeStats.mana >= Math.Abs(UpgradeStats.mECost) && Int32.Parse(manaEfficiencyUpgradeAmount.GetComponent<Text>().text.Substring(1)) < 5)
        {
            Text mECount = manaEfficiencyUpgradeAmount.GetComponent<Text>();
            UpgradeStats.IncManaEfficiency();
            int newVal = Int32.Parse(mECount.text.Substring(1)) + 1;
            mECount.text = "+" + newVal;
            currentManaHolder.GetComponent<Text>().text = UpgradeStats.mana.ToString();
        }
    }

    public void OnClickOSD()
    {
        if (UpgradeStats.mana >= Math.Abs(UpgradeStats.oSDCost))
        {
            Text oSDCount = spellDamageUpgradeAmount.GetComponent<Text>();
            UpgradeStats.IncSpellDamage();
            int newVal = Int32.Parse(oSDCount.text.Substring(1)) + 1;
            oSDCount.text = "+" + newVal;
            currentManaHolder.GetComponent<Text>().text = UpgradeStats.mana.ToString();
        }
    }
}
