using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

using ABOGGUS.Input;
using ABOGGUS.Gameplay;
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

        public void Initialize(Input.InputActions playerActions)
        {
            if (debug || GameController.player == null)
            {
                GameController.player = this;

                playerController.Initialize(playerActions, physicalGameObject);

                inventory = new PlayerInventory();
            }
        }

        public void TakeDamage(float damage)
        {
            inventory.TakeDamage(damage);
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
            this.playerController._FixedUpdate();
        }

        public void SetController(PlayerController playerController)
        {
            this.playerController = playerController;
        }

        public void SetGameObject(GameObject physicalGameObject)
        {
            this.playerController.SetGameObject(physicalGameObject);
        }

        public GameObject GetGameObject()
        {
            return this.playerController.GetGameObject();
        }
    }
}
