using System;

using UnityEngine;
using Unity;

using ABOGGUS.PlayerObjects;
using ABOGGUS.SaveSystem;
using UnityEngine.SceneManagement;
using System.Collections;

namespace ABOGGUS.Gameplay
{
    public class PlayerUpdater : MonoBehaviour
    {
        public void Awake()
        {
            if (GameController.playerUpdater is not null)
            {
                Destroy(gameObject);
                return;
            }
            GameController.playerUpdater = this;
        }

        public void FixedUpdate()
        {
            //Debug.Log("Game controller's player updater: " + GameController.playerUpdater);
        }

        public void UpdatePhysicalGameObjectForPlayer(string scene, bool loadingPlayer)
        {
            StartCoroutine(waitForSceneLoad(scene, loadingPlayer));
        }

        IEnumerator waitForSceneLoad(string scene, bool loadingPlayer)
        {
            while (SceneManager.GetActiveScene().name != scene || GameController.player == null) yield return null;

            Player player = GameController.player;

            GameObject physicalGameObject = GameObject.Find(PlayerConstants.GAMEOBJECT_PLAYERNAME);
            player.SetGameObject(physicalGameObject);
            player.inventory.invulnerable = false;

            if (loadingPlayer)
            {
                SaveGameManager.LoadPlayerProgress(GameController.player);
            }
            SaveGameManager.SavePlayerProgress(GameController.player);  // autosave for going between different play scenes

            Debug.Log("Successfully updated player game object");
        }
    }
}
