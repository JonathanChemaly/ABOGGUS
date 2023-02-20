using System;

using UnityEngine;
using Unity;

using ABOGGUS.PlayerObjects;
using ABOGGUS.SaveSystem;
using UnityEngine.SceneManagement;
using System.Collections;

namespace ABOGGUS.Gameplay
{
    internal class PlayerUpdater : MonoBehaviour
    {
        public void Awake()
        {
            GameController.playerUpdater = this;
        }

        public void UpdatePhysicalGameObjectForPlayer(string scene)
        {
            StartCoroutine(waitForSceneLoad(scene));
        }

        IEnumerator waitForSceneLoad(string scene)
        {
            while (SceneManager.GetActiveScene().name != scene || GameController.player == null) yield return null;

            GameObject physicalGameObject = GameObject.Find(PlayerConstants.GAMEOBJECT_PLAYERNAME);
            GameController.player.SetGameObject(physicalGameObject);
            GameController.player.inventory.invulnerable = false;

            SaveGameManager.SavePlayerProgress(GameController.player);

            Debug.Log("Successfully updated player game object");
        }
    }
}
