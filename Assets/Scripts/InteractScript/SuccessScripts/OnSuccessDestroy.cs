using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.Interact
{
    public class OnSuccessDestroy : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The name of the object to appear in UI when looked at")]
        private Interactable item;

        // Start is called before the first frame update
        void Start()
        {
            item.SuccessAction += DestoryItem;
        }

        void DestoryItem()
        {
            Destroy(gameObject);
        }
    }
}

