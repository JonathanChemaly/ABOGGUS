using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ABOGGUS.Interact
{
    public class ChangeObjectLayerOnInteract : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("interact to watch")]
        private Interactable interact;

        [SerializeField]
        [Tooltip("GameObject To change layer of")]
        private GameObject gObject;

        [SerializeField]
        [Tooltip("name of layer in string form to change to")]
        private string layerName;


        // Start is called before the first frame update
        void Start()
        {
            interact.InteractAction += ChangeLayer;
        }

        private void ChangeLayer()
        {
            int LayerNumber = LayerMask.NameToLayer(layerName);
            gObject.layer = LayerNumber;
            interact.DoSuccesAction();
        }
    }
}

