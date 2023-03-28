using System.Collections;
using System.Collections.Generic;

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

        public PlayerInventory inventory;
        public PlayerHUD playerHUD;
        public float invulnerabilityFrames = PlayerConstants.INVULNERABILITY_FRAMES;

        private static bool exists = false;

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
        }

        public void TakeDamage(float damage)
        {
            if (invulnerabilityFrames == 0)
            {
                inventory.TakeDamage(damage);
                invulnerabilityFrames = PlayerConstants.INVULNERABILITY_FRAMES;
            }

            playerHUD.UpdateHealthBar();
        }
        public void updateMana(int value)
        {
            inventory.mana += value;
            playerHUD.UpdateMana();
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

        private void OnEnable()
        {
            //PlayerDied += GameController.Respawn;
            PlayerDied += GameOverMenu.ActivateGameOver;
        }
        private void OnDisable()
        {
            //PlayerDied -= GameController.Respawn;
            PlayerDied -= GameOverMenu.ActivateGameOver;
        }
    }
}
