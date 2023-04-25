using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.Interact.Puzzles.RunePuzzle
{
    public class SetupRay : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Interactable Associated with this action")]
        private Interactable runeInteractable;

        [SerializeField]
        [Tooltip("Ray Associated with this")]
        private GameObject ray;

        [SerializeField]
        [Tooltip("Where the rotation should point at")]
        private GameObject target;

        private void Start()
        {
            ray.SetActive(false);
            if(runeInteractable != null)
            {
                runeInteractable.InteractAction += EnableRay;
            }
            else
            {
                EnableRay();
            }
        }

        public void EnableRay()
        {
            ray.SetActive(true);

            Transform rayT = ray.transform;

            rayT.LookAt(target.transform);

            //rayT.rotation = Quaternion.Euler(new Vector3(360 - rayT.rotation.eulerAngles.x, 
            //rayT.rotation.eulerAngles.y, rayT.rotation.eulerAngles.z));
            
            /*
            Vector3 relativePos = target.transform.position - rayT.position;

            Debug.Log(relativePos);

            Quaternion newRotation = Quaternion.LookRotation(relativePos, Vector3.up);

            Debug.Log(newRotation.eulerAngles);

            rayT.rotation = newRotation;
            */
        }
    }
}

