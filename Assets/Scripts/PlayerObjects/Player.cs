using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

using ABOGGUS.Input;
using ABOGGUS.Gameplay;
using ABOGGUS.Menus;
using System;

namespace ABOGGUS.PlayerObjects
{
    public class Player : MonoBehaviour
    {
        public GameObject physicalGameObject;
        public PlayerController playerController;

        public GameObject gameOverText;
        public bool debug = false;

        public static Action PlayerDied;
        public static Action WeaponChanged;

        public PlayerInventory inventory;
        public PlayerHUD playerHUD;
        public float invulnerabilityFrames = PlayerConstants.INVULNERABILITY_FRAMES;

        private static bool exists = false;
        private bool resist = false;

        public void Awake()
        {
            if (debug || !exists)
            {
                GameController.player = this;
                playerController = this.transform.GetComponent<PlayerController>();

                if (debug)
                {
                    GameObject physicalGameObject = GameObject.Find(PlayerConstants.GAMEOBJECT_PLAYERNAME);
                    SetGameObject(physicalGameObject);
                }

                playerController.InitializeForPlayer();
                inventory = new PlayerInventory();
                SetHUD();
                exists = true;
                DontDestroyOnLoad(gameObject);
            }
        }

        private void SetHUD()
        {
            GameObject hudObj = physicalGameObject.transform.Find("HUD").gameObject;
            playerHUD = hudObj.GetComponent<PlayerHUD>();
            playerHUD.playerInventory = this.inventory;
            var tempScale = playerHUD.transform.Find("HealthBar").localScale;
            tempScale.x = UpgradeStats.healthBarSize;
            playerHUD.transform.Find("HealthBar").localScale = tempScale;
            hudObj.transform.Find("ManaBar").gameObject.transform.Find("ManaValue").GetComponent<TextMeshProUGUI>().text = UpgradeStats.mana.ToString();
            playerHUD.UpdateWeapon(playerController.GetCurrentWeapon());
        }

        public void TakeDamage(float damage)
        {
            if (resist)
            {
                damage = damage / 2;
            }
            if (damage > 0)
            {
                if (invulnerabilityFrames == 0)
                {
                    inventory.TakeDamage(damage);
                    invulnerabilityFrames = PlayerConstants.INVULNERABILITY_FRAMES;
                }
            }
            else
            {
                inventory.TakeDamage(damage);
            }

            playerHUD.UpdateHealthBar();
        }
        public void updateMana(int value)
        {
            inventory.mana += value;
            UpgradeStats.mana += value;
            if (value > 0)
            {
                inventory.totalMana += value;
                UpgradeStats.totalMana += value;
            }
            playerHUD.UpdateMana();
        }

        public void UpdateWeapon()
        {
            playerHUD.UpdateWeapon(playerController.GetCurrentWeapon());
        }
        IEnumerator ToCredits()
        {
            //gameOverText.SetActive(true);
            Time.timeScale = 0;
            yield return new WaitForSeconds(5f);
            GameController.QuitGame("Player died lol.");

        }

        void FixedUpdate()
        {
            if (debug) _FixedUpdate();
        }
        public void _FixedUpdate()
        {
            if (this.playerController != null) { this.playerController._FixedUpdate(); }
            if (invulnerabilityFrames > 0) invulnerabilityFrames--;
        }
        
        public void SetController(PlayerController playerController)
        {
            this.playerController = playerController;
        }

        public void SetGameObject(GameObject physicalGameObject)
        {
            this.physicalGameObject = physicalGameObject;
            //this.playerController.SetGameObject(physicalGameObject);
            this.playerController.InitializePlayerState(physicalGameObject);
            SetHUD();
        }

        public GameObject GetGameObject()
        {
            return this.playerController.GetGameObject();
        }

        public void SetResistance(bool resist)
        {
            this.resist = resist;
        }

        private void OnEnable()
        {
            //PlayerDied += GameController.Respawn;
            PlayerDied += GameOverMenu.ActivateGameOver;
            WeaponChanged += this.UpdateWeapon;
        }
        private void OnDisable()
        {
            //PlayerDied -= GameController.Respawn;
            PlayerDied -= GameOverMenu.ActivateGameOver;
            WeaponChanged -= this.UpdateWeapon;
        }
    }
}
