using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ABOGGUS.PlayerObjects;

namespace ABBOGGUS.Menus {
    public class StatsMenu : MonoBehaviour
    {
        [SerializeField] private GameObject statsBox;
        private Text maxHealth;
        private Text currentHealth;
        private Text swordDamage;
        private Text spearDamage;
        private Text totalMana;
        private Text currentMana;
        private Text dungeonRuns;
        private Text manaEfficiency;
        private Text bonusHealthFromMana;
        private Text spellDam;
        // Start is called before the first frame update
        void Start()
        {
            maxHealth = transform.Find("Max Health").Find("LineHolder").Find("CurAmount").gameObject.GetComponent<Text>();
            currentHealth = transform.Find("Current Health").Find("LineHolder").Find("CurAmount").gameObject.GetComponent<Text>();
            swordDamage = transform.Find("Sword Damage").Find("LineHolder").Find("CurAmount").gameObject.GetComponent<Text>();
            spearDamage = transform.Find("Spear Damage").Find("LineHolder").Find("CurAmount").gameObject.GetComponent<Text>();
            totalMana = transform.Find("Total Mana").Find("LineHolder").Find("CurAmount").gameObject.GetComponent<Text>();
            currentMana = transform.Find("Current Mana").Find("LineHolder").Find("CurAmount").gameObject.GetComponent<Text>();
            dungeonRuns = transform.Find("Dungeon Runs").Find("LineHolder").Find("CurAmount").gameObject.GetComponent<Text>();
            manaEfficiency = transform.Find("Mana Efficiency").Find("LineHolder").Find("CurAmount").gameObject.GetComponent<Text>();
            spellDam = transform.Find("Overall Spell Damage").Find("LineHolder").Find("CurAmount").gameObject.GetComponent<Text>();
            bonusHealthFromMana = transform.Find("Bonus Health From Mana").Find("LineHolder").Find("CurAmount").gameObject.GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            maxHealth.text = GameObject.Find("PlayerScripts").GetComponent<Player>().inventory.maxHealth.ToString();
            currentHealth.text = GameObject.Find("PlayerScripts").GetComponent<Player>().inventory.health.ToString();
            swordDamage.text = WeaponDamageStats.swordDamage.ToString();
            spearDamage.text = WeaponDamageStats.spearDamage.ToString();
            totalMana.text = UpgradeStats.totalMana.ToString();
            currentMana.text = UpgradeStats.mana.ToString();
            dungeonRuns.text = UpgradeStats.runs.ToString();
            manaEfficiency.text = UpgradeStats.manaEfficiency.ToString("0.0");
            spellDam.text = UpgradeStats.overallSpellDamBonus.ToString();
            bonusHealthFromMana.text = UpgradeStats.healFromManaVal.ToString();
        }

        public void ExitStatsMenu()
        {
            statsBox.SetActive(false);
        }
    }
}
