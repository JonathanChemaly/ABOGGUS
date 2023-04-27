using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABOGGUS.PlayerObjects;
using ABOGGUS.PlayerObjects.Items;
using ABOGGUS.Gameplay;
using ABOGGUS.SaveSystem;

namespace ABOGGUS.Interact
{
    public class PickupInteractable : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("interact to watch")]
        private Interactable interact;

        [SerializeField]
        [Tooltip("name of bool field in player inventory to check if the player has (case-sensitive)")]
        private string itemName = "key";

        private PlayerInventory playerInv;
        private bool itemInfo;
        //private System.Reflection.PropertyInfo itemInfo;

        // Start is called before the first frame update
        void Start()
        {
            interact.InteractAction += pickUpObject;
            playerInv = GameController.player.inventory;
            //itemInfo = playerInv.GetType().GetProperty(itemName);
            StartCoroutine(DestroyIfInPlayerInv());
        }

        IEnumerator DestroyIfInPlayerInv()
        {
            while (!SaveGameManager.finishedLoadingPlayer)
            {
                yield return null;
            }
            itemInfo = playerInv.HasItem(itemName);
            if (itemInfo) Destroy(gameObject);

        }

        private void pickUpObject()
        {
            if (interact.CompareTag(ItemLookup.GrimoireName))
            {
                playerInv.AddItem(ItemLookup.GrimoireName);
            }
            else if (interact.CompareTag(ItemLookup.WheelName))
            {
                playerInv.AddItem(ItemLookup.WheelName);
            }
            else if (interact.CompareTag(ItemLookup.WrenchName))
            {
                playerInv.AddItem(ItemLookup.WrenchName);
            }
            else if (interact.CompareTag(ItemLookup.GasName))
            {
                playerInv.AddItem(ItemLookup.GasName);
            }
            else if (interact.CompareTag(ItemLookup.TractorKeyName))
            {
                playerInv.AddItem(ItemLookup.TractorKeyName);
            }
            else if (interact.CompareTag(ItemLookup.HammerName))
            {
                playerInv.AddItem(ItemLookup.HammerName);
            }
            else if (interact.CompareTag(ItemLookup.BucketName))
            {
                Debug.Log("Bucket item picked up");
                playerInv.AddItem(ItemLookup.BucketName);
            }
            //itemInfo.SetValue(playerInv, true);
            itemInfo = true;
            interact.DoSuccesAction();
        }
    }
}
